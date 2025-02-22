﻿using SIM.Core.Commands;
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
using System.Xml.Serialization;

namespace SIM.Shell
{
    class Program
    {
        private static SimNodeFactory factory;
        private static ISimRepository repository;
        private static Assembly assembly;

        static void Main(string[] args)
        {
            repository = new Neo4jRepository();
            var x = repository.GetAll(a => a is Node);
        }

        private static void Test()
        {
            //string userInput = string.Empty;

            //// Make factory
            //factory = new SimNodeFactory();

            //// Make a repository
            ////repository = new Repository();
            //repository = new Neo4jRepository();

            //SIM.Aibel.CAR.SDI sdi = new Aibel.CAR.SDI();
            //sdi.DataCode = "SDI-01";
            //sdi.Revision = 1;
            //sdi.Subject = "First SDI for CAR";
            //sdi.StatusDate = DateTime.Now;
            //var x = sdi.Validate(new ValidationContext(sdi));
            //var y = Validator.TryValidateObject(sdi, new ValidationContext(sdi), null);

            //SIM.Aibel.CAR.Release release = new Aibel.CAR.Release();
            //y = Validator.TryValidateObject(release, new ValidationContext(release), null);

            //// Load dll
            //assembly = AppDomain.CurrentDomain.Load(args[0]);

            //// Get user input for making an instance
            ////userInput = Console.ReadLine();
            //Graph g = new Graph();

            //Type type = GetUserType("SDI");
            //var moveArgs = GetArgs(type);
            //moveArgs.AssignArgumentValues();
            //moveArgs.AssignArgumentValues(30);
            //var move = Create(type, moveArgs);
            //g.Nodes.Add(move);

            //type = GetUserType("Section");
            //var moveArgs2 = GetArgs(type);
            ////moveArgs2.AssignArgumentValues(new IntegerPropertyNode(3));
            //moveArgs2.AssignArgumentValues(30);
            //var move2 = Create(type, moveArgs2);
            //g.Nodes.Add(move2);

            //type = GetUserType("HasSection");
            //var sessionArgs = GetArgs(type);
            ////sessionArgs.AssignArgumentValues(new DateTimePropertyNode(DateTime.Now));
            //sessionArgs.AssignArgumentValues(DateTime.Now);
            //var session = Create(type, sessionArgs);
            //g.Nodes.Add(session);

            //type = GetUserType("Issued_By");
            //var hasSession = move.RelateTo(type, session, true);
            //g.Relations.Add(hasSession);

            //type = GetUserType("HasSession");
            //var hasSession2 = move2.RelateTo(type, session, true);
            //g.Relations.Add(hasSession2);

            //type = GetUserType("HasMove");
            //var hasMove = move.RelateTo(type, session, false);
            //g.Relations.Add(hasMove);

            //type = GetUserType("HasMove");
            //var hasMove2 = move2.RelateTo(type, session, false);
            //g.Relations.Add(hasMove2);

            //repository.Add(g);
        }

        private static void Aibel()
        {

            Type type = GetUserType("Section");
            var section = Create(type, GetArgs(type));
            repository.Add(section);

            type = GetUserType("SDI");
            var arg = GetArgs(type);
            var sdi = Create(type, arg);
            //arg.AssignArgumentValues(section, new DateTimePropertyNode(DateTime.Now));
            //var sdi = Create(type, arg);
            repository.Add(sdi);

            type = GetUserType("CO");
            arg = GetArgs(type);
            var res = new List<INode>();
            for (int i = 0; i < arg.Arguments.Length; i++)
            {
                Console.Write(arg.Arguments[i].Name + ": ");
                var s = Console.ReadLine();
                if (arg.Arguments[i].PropertyType == typeof(Relation))
                {
                    res.Add(section);
                }
                else
                {
                    //res.Add(new StringPropertyNode(s));
                }
            }
            arg.AssignArgumentValues(res.ToArray());
            var co = Create(type, arg);
            repository.Add(co);

            type = GetUserType("HasCO");
            var rel = sdi.RelateTo(type, co, true);
            repository.Add(rel);

            var x = sdi.Relations(repository);
            var y = co.Relations(repository);
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
