﻿//using SIM.Shell.Core;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Interfaces;
using SIM.Shell.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Admin
{
    class Program
    {
        private static CommandManager commander;

        static void Main(string[] args)
        {
            commander = new CommandManager();

            var listener = new Listener() { ContinueListening = true };
            listener.Listened += AnalyzeCommand;

            while (listener.ContinueListening)
            {
                listener.Listen();
            }
        }

        private static void AnalyzeCommand(string userInput)
        {
            CommandAnalyser analyser = new CommandAnalyser(commander, userInput);
            object[] arguments = null;
            if (analyser.GetRequiredArguments(out var parameters))
                arguments = PrintArguments(parameters);

            var x = analyser.Execute(arguments);
            var repository = new AdminRepository();
            if (repository.Get(a => (a as DynamicObject).Name == (x as DynamicObject).Name) == null)
                repository.Add(x as ISimObject);
            //else
            //    repository.Update(x as ISimObject);
        }

        private static object[] PrintArguments(IDictionary<string, Type> parameters)
        {
            List<object> result = new List<object>();
            for (int i = 0; i < parameters.Count; i++)
            {
                // Check if repository is required
                if (parameters.Values.ElementAt(i) == typeof(ISimRepository))
                {
                    result.Add(new AdminRepository());
                    continue;
                }                    

                var response = $"Arg[{i}]:\nName of argument: {parameters.Keys.ElementAt(i)}\n" +
                    $"Type of data: {parameters.Values.ElementAt(i)}";
                Responder.Respond(response);
                var listener = new Listener();
                listener.Listened += a => result.Add(a);
                listener.Listen();
            }
            return result.ToArray();
        }
    }
}
