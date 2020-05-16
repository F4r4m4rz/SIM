using SIM.CodeEngine.Dynamic;
using SIM.Core.Commands;
using SIM.Core.Objects;
using SIM.Shell.Core;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SIM.DataBase;

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
            if (x is IEnumerable<DynamicObject>)
            {
                var jsonRepos = new AdminRepository(x as IEnumerable<DynamicObject>);
                return;
            }
            var repository = new AdminRepository();
            if (x != null && repository.Get(a => (a as DynamicObject).Name == (x as DynamicObject).Name) == null)
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
                var paramType = parameters.Values.ElementAt(i);
                var response = $"Arg[{i}]:\nName of argument: {parameters.Keys.ElementAt(i)}\n" +
                    $"Type of data: {paramType}";
                Responder.Respond(response);
                var listener = new Listener();
                if (paramType.GetInterface(typeof(IEnumerable).Name) == null || paramType == typeof(string))
                    listener.Listened += a => result.Add(a);
                else
                    listener.Listened += a => result.Add((a as string).Split(' '));

                listener.Listen();
            }
            return result.ToArray();
        }
    }
}
