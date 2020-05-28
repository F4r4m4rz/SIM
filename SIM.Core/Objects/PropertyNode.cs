using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class PropertyNode : INode
    {
        protected object _value;

        protected internal PropertyNode(object value)
        {
            _value = value;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { ValidationResult.Success };
        }
    }
}
