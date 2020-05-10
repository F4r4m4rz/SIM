using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Core
{
    public class CommandAnalyser
    {
        public event NotValidCommandEvent NotValidEvent;

        private readonly ICommandManager commandManager;
        private string userInput;

        public CommandAnalyser(ICommandManager commandManager ,string userInput)
        {
            this.commandManager = commandManager ?? throw new ArgumentNullException(nameof(commandManager));
            this.userInput = userInput;
        }

        public bool GetRequiredArguments(out IDictionary<string, Type> arguments)
        {
            arguments = new Dictionary<string, Type>();
            var com = commandManager.GetCommand(userInput);
            var constructor = com.GetConstructors()
                .Where(a=>a.IsPublic)
                .FirstOrDefault();
            var parameters = constructor.GetParameters();

            if (parameters.Length == 0) return false;

            for (int i = 0; i < parameters.Length; i++)
            {
                arguments.Add(parameters[i].Name, parameters[i].ParameterType);
            }

            return true;
        }

        public void ValidateCommand()
        {

        }

        public object Execute(params object[] parameters)
        {
            var comType = commandManager.GetCommand(userInput);
            var com = Activator.CreateInstance(comType, parameters) as ISimCommand;
            com.Execute();
            return com.Result;
        }
    }
}
