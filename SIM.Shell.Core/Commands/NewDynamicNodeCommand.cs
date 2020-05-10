using SIM.CodeEngine.Dynamic;
using SIM.Core.Abstractions;
using SIM.Core.Attributes;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Core.Commands
{
    [AdminCommand]
    [CommandString("dynamicnode")]
    public class NewDynamicNodeCommand : ISimCommand
    {
        private readonly string nameSpace;
        private readonly string name;

        public object Result { get; set; }

        public NewDynamicNodeCommand(string nameSpace, string name)
        {
            this.nameSpace = nameSpace;
            this.name = name;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            Result = new DynamicNode(nameSpace, name);
        }
    }
}
