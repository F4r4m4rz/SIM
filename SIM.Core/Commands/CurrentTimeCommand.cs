using SIM.Core.Objects;
using SIM.Core.Attributes;
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
        public CurrentTimeCommand(DateTimePropertyNode node) 
            : base(node, DateTime.Now)
        {

        }
    }
}
