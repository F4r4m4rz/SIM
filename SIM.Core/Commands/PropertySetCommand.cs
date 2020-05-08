using SIM.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    public class PropertySetCommand : ISimPropertySetCommand
    {
        public virtual bool CanExecute(ISimObject obj, PropertyInfo prop, object value)
        {
            return true;
        }

        public virtual void Execute(ISimObject obj, PropertyInfo prop, object value)
        {
            if (CanExecute(obj, prop, value))
                prop.SetValue(obj, value);

            throw new OperationCanceledException($"{prop.Name} cannot be set on {obj}\nRequested value is {value}",
                                                 new CancellationToken(true));
        }
    }
}
