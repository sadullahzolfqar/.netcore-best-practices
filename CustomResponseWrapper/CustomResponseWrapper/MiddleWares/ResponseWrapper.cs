using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomResponseWrapper.MiddleWares
{
    public class APIResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public APIResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await this._next(context);
            }
            else
            {
                var currentBody = context.Response.Body;

                using (var memoryStream = new MemoryStream())
                {
                    //set the current response to the memorystream.
                    context.Response.Body = memoryStream;

                    try
                    {
                        await _next(context);
                        if(context.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            var body = await FormatResponse(context.Response);
                            var objResult = JsonConvert.DeserializeObject(body);
                            var result = new CommonApiResponse((HttpStatusCode)context.Response.StatusCode, objResult, "Success");
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                        }
                        else
                        {
                            await HandleNotSuccessRequestAsync(context, (HttpStatusCode)context.Response.StatusCode);
                        }
                    }
                    catch(Exception ex)
                    {
                        await HandleExceptionAsync(context, ex);
                    }
                    finally
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        await memoryStream.CopyToAsync(currentBody);
                    }

                }
            }
           
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            ApiError apiError = null;
            CommonApiResponse apiResponse = null;
            int code = 0;

            if (exception is ApiException)
            {
                var ex = exception as ApiException;
                apiError = new ApiError(ex.Message);
                apiError.ValidationErrors = ex.Errors;
                apiError.ReferenceErrorCode = ex.ReferenceErrorCode;
                apiError.ReferenceDocumentLink = ex.ReferenceDocumentLink;
                code = ex.StatusCode;
                context.Response.StatusCode = code;

            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                code = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
                #if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
                #else
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
                #endif

                apiError = new ApiError(msg);
                apiError.Details = stack;
                code = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";

            apiResponse = new CommonApiResponse((HttpStatusCode)code, null, ResponseMessageEnum.Exception.ToString(), apiError);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotSuccessRequestAsync(HttpContext context, HttpStatusCode code)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError = null;
            CommonApiResponse apiResponse = null;

            if (code == HttpStatusCode.NotFound)
                apiError = new ApiError("The specified URI does not exist. Please verify and try again.");
            else if (code == HttpStatusCode.NoContent)
                apiError = new ApiError("The specified URI does not contain any content.");
            else
                apiError = new ApiError("Your request cannot be processed. Please contact a support.");

            apiResponse = new CommonApiResponse(code, null, ResponseMessageEnum.Failure.ToString(), apiError);
            context.Response.StatusCode = (int)code;

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");

        }
    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIResponseMiddleware>();
        }
    }


    public class CommonApiResponse
    {
        //public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        //{
        //    return new CommonApiResponse(statusCode, result, errorMessage);
        //}


        public string Version { get; set; }

        public int StatusCode { get; set; }
        public string RequestId { get; }

        public string Message { get; set; }

        public object Result { get; set; }

        public ApiError ResponseException { get; set; }

        public CommonApiResponse(HttpStatusCode statusCode, object result = null, string message = "", ApiError apiError = null, string apiVersion = "1.0.0.0")
        {
            this.StatusCode = (int)statusCode;
            this.Message = message;
            this.Result = result;
            this.ResponseException = apiError;
            this.Version = apiVersion;
        }
    }

    public class ApiError
    {
        public bool IsError { get; set; }
        public string ExceptionMessage { get; set; }
        public string Details { get; set; }
        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }

        public ApiError(string message)
        {
            this.ExceptionMessage = message;
            this.IsError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            this.IsError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                this.ExceptionMessage = "Please correct the specified validation errors and try again.";
                this.ValidationErrors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();

            }
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }

        public ApiException(string message,
                            int statusCode = 500,
                            IEnumerable<ValidationError> errors = null,
                            string errorCode = "",
                            string refLink = "") :
            base(message)
        {
            this.StatusCode = statusCode;
            this.Errors = errors;
            this.ReferenceErrorCode = errorCode;
            this.ReferenceDocumentLink = refLink;
        }

        public ApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

    public enum ResponseMessageEnum
    {
        [Description("Request successful.")]
        Success,
        [Description("Request responded with exceptions.")]
        Exception,
        [Description("Request denied.")]
        UnAuthorized,
        [Description("Request responded with validation error(s).")]
        ValidationError,
        [Description("Unable to process the request.")]
        Failure
    }
}
