using SIM.Core.Commands;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SIM.Core.Factory;
using SIM.Neo4j;
using SIM.DataBase;

namespace SIM.Shell
{
    class Program
    {
        private static SimNodeFactory factory;
        private static ISimRepository repository;
        private static Assembly assembly;

        static void Main(string[] args)
        {
            string userInput = string.Empty;

            // Make factory
            factory = new SimNodeFactory();

            // Make a repository
            //repository = new Repository();
            repository = new Neo4jRepository();

            // Load dll
            assembly = AppDomain.CurrentDomain.Load(args[0]);

            // Get user input for making an instance
            //userInput = Console.ReadLine();

            Type type = GetUserType("Section");
            var section = Create(type, GetArgs(type));
            repository.Add(section);

            type = GetUserType("SDI");
            var arg = GetArgs(type);
            var sdi = Create(type, arg);
            //arg.AssignArgumentValues(section, new DateTimePropertyNode(DateTime.Now));
            //var sdi = Create(type, arg);
            repository.Add(sdi);

            //type = GetUserType("CO");
            //arg = GetArgs(type);
            //var res = new List<INode>();
            //for (int i = 0; i < arg.Arguments.Length; i++)
            //{
            //    Console.Write(arg.Arguments[i].Name + ": ");
            //    var s = Console.ReadLine();
            //    if (arg.Arguments[i].PropertyType == typeof(Relation))
            //    {
            //        res.Add(section);
            //    }
            //    else
            //    {
            //        res.Add(new StringPropertyNode(s));
            //    }
            //}
            //arg.AssignArgumentValues(res.ToArray());
            //var co = Create(type, arg);
            //repository.Add(co);

            //type = GetUserType("HasCO");
            //var rel = sdi.RelateTo(type, co, true);
            //repository.Add(rel);

            //var x = sdi.Relations(repository);
            //var y = co.Relations(repository);
        }

        private static ISimNodeConstructionArgument GetArgs(Type type)
        {
            return factory.GetConstructionArguments(type);
        }

        private static Node Create(Type type, ISimNodeConstructionArgument args)
        {
            return factory.New(args);
        }

        private static Type GetUserType(string userInput)
        {
            return assembly.GetTypes().Where(a => a.Name == userInput).FirstOrDefault();
        }
    }
}
