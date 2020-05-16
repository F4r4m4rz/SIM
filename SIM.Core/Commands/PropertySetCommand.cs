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
        protected readonly ProperyNode node;
        protected readonly object value;

        public object Result { get; private set; }

        public PropertySetCommand(ProperyNode node, object value)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public virtual bool CanExecute()
        {
            return true;
        }

        public virtual void Execute()
        {
            if (!CanExecute())
                throw new OperationCanceledException($"Cannot set value on {node}\nRequested value is {value}",
                                                 new CancellationToken(true));

            node.SetValue(value);
            Result = "Success";
        }
    }
}
