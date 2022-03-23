using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ResponseService.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController: ControllerBase
    {
        //GET /api/response/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult GetAResponse(int id)
        {
            Random random = new Random();
            var rndInteger = random.Next(1, 101);
            if(rndInteger >= id)
            {
                Console.WriteLine("--> Failure Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Console.WriteLine("--> Success Generate a HTTP 200");
            return Ok();
        }
    }
}