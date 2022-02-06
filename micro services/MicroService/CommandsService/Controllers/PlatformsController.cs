using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
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
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;
        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Gettting Platforms from commands service");

            var items = repository.GetAllPlatforms();

            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(items));
        }

        [HttpPost]
        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("---> Inbound Post # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
