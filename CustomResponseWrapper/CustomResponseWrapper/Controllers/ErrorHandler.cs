using CustomResponseWrapper.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomResponseWrapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorHandlerController : ControllerBase
    {
        [Route("{id}")]
        [HttpGet]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                int errorException = 4 / id;
            }
            else if (id >= 10 && id < 20)
            {
                throw new ApiException("Id can't be between 10 and 20");
            }
            else if (id > 1 && id < 10)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
