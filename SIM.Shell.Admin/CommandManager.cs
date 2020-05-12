using SIM.Core.Attributes;
using SIM.Core.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Admin
{
    public class CommandManager : ICommandManager
    {
        public ICollection<Type> Commands { get; }

        public CommandManager()
        {
            Commands = CollectAllCommands();
        }

        private ICollection<Type> CollectAllCommands()
        {
            List<Type> commands = new List<Type>();
            commands.AddRange(GetAllCommands());

            return commands;
        }

        private IEnumerable<Type> GetAllCommands()
        {
            var libraries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "SIM.*.dll").ToList();
            var commands = new List<Type>();
            commands.AddRange(GetAllCommands(AppDomain.CurrentDomain.GetAssemblies()));
            for (int i = 0; i < libraries.Count(); i++)
            {
                commands.AddRange(GetAllCommands(Assembly.Load(Path.GetFileNameWithoutExtension(libraries[i]))));
            }
            return commands.Distinct();
        }

        private IEnumerable<Type> GetAllCommands(Assembly[] assemblies)
        {
            var commands = new List<Type>();
            for (int i = 0; i < assemblies.Length; i++)
            {
                commands.AddRange(GetAllCommands(assemblies[i]));
            }
            return commands;
        }

        private IEnumerable<Type> GetAllCommands(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(a => a.GetInterfaces().Contains(typeof(ISimCommand)))
                .Where(a => a.GetCustomAttribute<AdminCommandAttribute>() != null);
        }

        public Type GetCommand(string keyword)
        {
            return Commands.Where(a => a.GetCustomAttribute<CommandStringAttribute>() != null)
                .Where(a=>a.GetCustomAttribute<CommandStringAttribute>().CommandString == keyword)
                .FirstOrDefault();
        }
    }
}
