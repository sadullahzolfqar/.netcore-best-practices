using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/platforms")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo platformRepo;
        private readonly IMapper mapper;
        private readonly ICommandDataClient commandDataClient;
        private readonly IMessageBusClient messageBusClient;
        public PlatformsController(
            IPlatformRepo platformRepo, 
            IMapper mapper, 
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            this.platformRepo = platformRepo;
            this.mapper = mapper;
            this.commandDataClient = commandDataClient;
            this.messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("---> Getting Platforms -----");

            var platformItems = platformRepo.GetAllPlatforms();

            return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int Id)
        {
            Console.WriteLine("---> Getting Platforms -----");

            var platformItem = platformRepo.GetPlatformById(Id);

            if(platformItem != null)
            {
                return Ok(mapper.Map<PlatformReadDto>(platformItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
        {
            Platform platformModel = mapper.Map<Platform>(platformCreateDto);

            platformRepo.CreatePlatform(platformModel);
            platformRepo.SaveChanges();

            PlatformReadDto platformReadDto = mapper.Map<PlatformReadDto>(platformModel);

            // send sync message
            try
            {
                await commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex) {
                Console.WriteLine($"--> could not send synchronously: {ex.Message}");
            }

            // send async message
            try
            {
                PlatformPublishDto platformPublishDto = mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishDto.Event = "PLATFORM_PUBLISHED";
                 
                messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}
