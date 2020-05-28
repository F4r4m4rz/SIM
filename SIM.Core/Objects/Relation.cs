using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class Relation : IGenericRelation<Node, Node>
    {
        protected Relation()
        {

        }

        /// <summary>
        /// The Node which lays at the start of the relation arrow
        /// </summary>
        public Node Origin { get; set; }

        /// <summary>
        /// The Node which lays at the end of the relation arrow
        /// </summary>
        public Node Target { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { ValidationResult.Success };
        }
    }
}
