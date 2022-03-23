using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy clientPolicy;

        public RequestController(ClientPolicy clientPolicy)
        {
            this.clientPolicy = clientPolicy;
        }

        //GET api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();

            //var response = await client.GetAsync("https://localhost:5001/api/response/30");
            var response = await clientPolicy.ImmediateHttpRetry.ExecuteAsync(
                () => client.GetAsync("https://localhost:5001/api/response/20")
                );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Response Sevice returned success");
                return Ok();
            }
            Console.WriteLine("--> Response Sevice returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
