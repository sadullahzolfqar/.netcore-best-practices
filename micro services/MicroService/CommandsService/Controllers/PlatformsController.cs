using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("---> Inbound Post # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
