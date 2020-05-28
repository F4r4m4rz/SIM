using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNodeTypeAttribute : ValidationAttribute
    {
        public PropertyNodeTypeAttribute(Type allowedType)
        {
            AllowedType = allowedType;
        }

        public Type AllowedType { get; }

        public override bool IsValid(object value)
        {
            var result = (value as ISimObject)?.Validate(new ValidationContext(this));
            return result == null || !result.Any(a=>a != null) ? true : false;
        }
    }
}
