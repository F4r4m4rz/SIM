using SIM.Core.Objects;
using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIM.Core.Commands
{
    [CommandString("propset")]
    public class PropertySetCommand : ISimCommand
    {
        private readonly ISimObject obj;
        private readonly PropertyInfo prop;
        private readonly object value;

        public object Result { get; private set; }

        public PropertySetCommand(ISimObject obj, PropertyInfo prop, object value)
        {
            this.obj = obj ?? throw new ArgumentNullException(nameof(obj));
            this.prop = prop ?? throw new ArgumentNullException(nameof(prop));
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        public virtual void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"{prop.Name} cannot be set on {obj}\nRequested value is {value}",
                                                 new CancellationToken(true));

            prop.SetValue(obj, value);
            Result = "Success";
        }
    }
}
