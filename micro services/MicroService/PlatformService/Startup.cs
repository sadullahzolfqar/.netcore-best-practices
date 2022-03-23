using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpcs;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //if (_env.IsProduction())
            //{
            //    Console.WriteLine("---> Using SqlServer Db 003");
            //    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            //    services.AddDbContext<AppDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("PlatformsConn").ToString(), serverVersion));
            //}
            //else
            //{
            //    Console.WriteLine("---> Using InMem Db");

            //}

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("IMem"));

            services.AddScoped<IPlatformRepo, PlatformRepo>();

            services.AddScoped<ICommandDataClient, HttpCommandDataClient>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddGrpc();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GrpcPlatformService>();
                
                endpoints.MapGet("/protos/platforms.proto", async context => {
                    await context.Response.WriteAsync(System.IO.File.ReadAllText("Protos/platforms.proto"));
                });
            });

            PrepDb.PrepPopulation(app, false);
        }
    }
}
