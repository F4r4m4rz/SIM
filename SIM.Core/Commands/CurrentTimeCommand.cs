using SIM.Core.Abstractions;
using SIM.Core.Attributes;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    [CommandString("curtime")]
    public class CurrentTimeCommand : PropertySetCommand
    {
        public CurrentTimeCommand(ISimObject obj, PropertyInfo prop, DateTime value) 
            : base(obj, prop, value)
        {

        }

        public override void Execute()
        {
            if (base.CanExecute())
                base.Execute();
        }
    }
}
