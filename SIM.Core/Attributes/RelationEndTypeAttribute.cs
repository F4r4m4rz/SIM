using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RelationEndTypeAttribute : Attribute
    {
        public RelationEndTypeAttribute(Type type, RelationEndEnum end)
        {
            Type = type;
            End = end;
        }

        public Type Type { get; }
        public RelationEndEnum End { get; }
    }
}
