
using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
  public class PlatformDataClient : IPlatformDataClient
  {
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
      this.configuration = configuration;
      this.mapper = mapper;
    }
    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Console.WriteLine($"--> Calling Grpc Service {configuration["GrpcPlatform"]}");

        var channel = GrpcChannel.ForAddress(configuration["GrpcPlatform"]);

        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
          var reply = client.GetAllPlatforms(request);

          return mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch(Exception ex)
        {
          Console.WriteLine($"--> Couldnot call grpc server: {ex.Message}");
          return null;
        }
    }
  }
}
