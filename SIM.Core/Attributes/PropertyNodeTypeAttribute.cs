using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNodeTypeAttribute : Attribute
    {
        public PropertyNodeTypeAttribute(Type allowedType)
        {
            AllowedType = allowedType;
        }

        public Type AllowedType { get; }
    }
}
