using CommandsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext context;
        public CommandRepo(AppDbContext context)
        {
            this.context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;

            context.Commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }

            context.Platforms.Add(plat);
        }

        public bool ExternalPlatformExist(int externalPlatformId)
        {
            return context.Platforms.Any(x => x.ExternalID == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatforms(int platformId)
        {
            return context.Commands.Where(x => x.PlatformId == platformId).OrderBy(x => x.Platform.Name);
        }

        public bool PlatformExits(int platformId)
        {
            return context.Platforms.Any(x => x.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0); 
        }
    }
}
