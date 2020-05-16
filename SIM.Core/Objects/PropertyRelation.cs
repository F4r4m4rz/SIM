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
    public abstract class PropertyRelation : IGenericRelation<Node, ProperyNode>
    {
        public Node Origin { get; set; }

        public ProperyNode Target { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
