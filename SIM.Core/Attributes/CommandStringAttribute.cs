using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CommandStringAttribute : Attribute
    {
        public CommandStringAttribute(string commandString)
        {
            CommandString = commandString;
        }

        public string CommandString { get; }
    }
}
