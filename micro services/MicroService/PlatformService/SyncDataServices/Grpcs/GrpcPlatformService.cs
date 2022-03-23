using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpcs
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
      private readonly IPlatformRepo repository;
      private readonly IMapper mapper;

      public GrpcPlatformService(IPlatformRepo repository, IMapper mapper)
      {
        this.repository = repository;
        this.mapper = mapper;
      }

      public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context) 
      {
        var response = new PlatformResponse();
        var platforms = repository.GetAllPlatforms();

        foreach(var plat in platforms)
        {
          response.Platform.Add(mapper.Map<GrpcPlatformModel>(plat));
        }

        return Task.FromResult(response);
      }
    }
}