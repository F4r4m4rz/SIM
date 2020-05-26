using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyRelationTargetTypeAttribute : Attribute
    {
        public PropertyRelationTargetTypeAttribute(Type targetType)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public Type TargetType { get; }
    }
}
