using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo repository;
        private readonly IMapper mapper;
        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForPlatforms(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");
            if (!repository.PlatformExits(platformId))
            {
                return NotFound();
            }

            var commands = repository.GetCommandsForPlatforms(platformId);

            return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandCreateDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");
            if (!repository.PlatformExits(platformId))
            {
                return NotFound();
            }

            var command = repository.GetCommand(platformId, commandId);

            return Ok(mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");
            if (!repository.PlatformExits(platformId))
            {
                return NotFound();
            }

            var command = mapper.Map<Command>(commandDto);
            repository.CreateCommand(platformId, command);
            repository.SaveChanges();

            var commandCreateDto = mapper.Map<CommandReadDTO>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandCreateDto.Id}, commandCreateDto);
        }

    }
}
