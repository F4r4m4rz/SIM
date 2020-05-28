using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    [SoloRelation(IsTargetSolo = true)]
    public class PropertyRelation : IGenericRelation<Node, PropertyNode>
    {
        public Node Origin { get; set; }

        public PropertyNode Target { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check PropertyNodeTypeAttribute
            var attribute = validationContext.ObjectInstance as PropertyNodeTypeAttribute;
            if (attribute != null && Target.GetType() != attribute.AllowedType)
                return new ValidationResult[] { new ValidationResult("Type mismatch") };

            return new ValidationResult[] { ValidationResult.Success };
        }
    }
}
