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
        static void Main(string[] args)
        {
            var listener = new Listener();
            listener.Listened += AnalyzeCommand;

            while (true)
            {
                listener.Listen();
            }
        }

        private static void AnalyzeCommand(string userInput)
        {
            CommandAnalyser analyser = new CommandAnalyser(userInput);
        }
    }
}
