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
    public class CurrentTimeCommand : ISimPropertySetCommand
    {
        public bool CanExecute(ISimObject obj, PropertyInfo prop)
        {
            return true;
        }

        public void Execute(ISimObject obj, PropertyInfo prop)
        {
            if (CanExecute(obj, prop))
                prop.SetValue(obj, DateTime.Now);

            throw new OperationCanceledException($"{prop.Name} cannot be set on {obj}", new CancellationToken(true));
        }
    }
}
