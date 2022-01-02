using EFinansConnector;
using EFinansUserConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WcfClientService.DespatchService.HTTP;
using WcfClientService.DespatchService.HTTP.Request;

namespace WcfClientService.DespatchService.EFinans
{
    public class EFinansConnector : IDespatchConnector
    {
        private EFinansTools eFinansTools;
        private UserServiceClient userService;
        private ConnectorServiceClient connectorService;
        CreateClientRequest clientRequest;
        string SessionID = string.Empty;

        public EFinansConnector()
        {
            
        }
        public EFinansConnector(CreateClientRequest clientRequest)
        {
            this.clientRequest = clientRequest;
            eFinansTools = new EFinansTools();

            BasicHttpBinding vidiBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            vidiBinding.AllowCookies = true;
            vidiBinding.MaxReceivedMessageSize = 104857600;

            BasicHttpBinding vidiBindingConnector = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            vidiBindingConnector.AllowCookies = true;
            vidiBindingConnector.MaxBufferPoolSize = int.MaxValue;
            vidiBindingConnector.MaxReceivedMessageSize = int.MaxValue;


            EndpointAddress userEndPointAddress = new EndpointAddress(clientRequest.userServiceUri);
            userService = new UserServiceClient(vidiBinding, userEndPointAddress);

            EndpointAddress endPointAddress = new EndpointAddress(clientRequest.serviceUri);
            connectorService = new ConnectorServiceClient(vidiBindingConnector, endPointAddress);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            using (new OperationContextScope(userService.InnerChannel))
            {
                userService.wsLogin(clientRequest.userName, clientRequest.passWord, clientRequest.langCode);

                MessageProperties props = OperationContext.Current.IncomingMessageProperties;
                HttpResponseMessageProperty prop = props[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
                SessionID = prop.Headers[HttpResponseHeader.SetCookie];
            }
        }

        public async Task<DespatchResult> GetOutBoxDespatch(GetOutBoxDespatchRequest request)
        {
            return await Task.FromResult<DespatchResult>(GetOutBoxDespacthCustom(request));
        }

        private DespatchResult GetOutBoxDespacthCustom(GetOutBoxDespatchRequest request)
        {
            DespatchResult result = new DespatchResult();

            try
            {
                using (new OperationContextScope(connectorService.InnerChannel))
                {
                    HttpRequestMessageProperty httpRequest = new HttpRequestMessageProperty();
                    httpRequest.Headers[HttpRequestHeader.Cookie] = SessionID;
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequest;

                    string[] belgeEttn = new string[] { request.despatchId };
                    byte[] despacthResponse = connectorService.gidenBelgeleriIndirEttn(clientRequest.vkn, belgeEttn, "IRSALIYE", "PDF");

                    byte[] responseData = eFinansTools.Decompress(despacthResponse).First().Value;

                    result.result = Convert.ToBase64String(responseData);
                    result.isSuccess = true;
                }
            }
            catch (System.Exception ex)
            {
                result.isSuccess = false;
                result.message = ex.ToString();
            }

            return result;
        }
    }
}
