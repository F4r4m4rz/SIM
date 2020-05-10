using SIM.Core.Attributes;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
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
            Commands = RegisterCommands();
        }

        private ICollection<Type> RegisterCommands()
        {
            List<Type> commands = new List<Type>();
            commands.AddRange(GetAllCommands());

            return commands;
        }

        private IEnumerable<Type> GetAllCommands()
        {
            return Assembly.Load("SIM.Shell.Core")
                .GetTypes()
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
