using SIM.Core.Abstractions;
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
    public class CurrentTimeCommand : PropertySetCommand
    {
        public override void Execute(ISimObject obj, PropertyInfo prop, object value)
        {
            if (base.CanExecute(obj, prop, DateTime.Now))
                base.Execute(obj, prop, DateTime.Now);
        }
    }
}
