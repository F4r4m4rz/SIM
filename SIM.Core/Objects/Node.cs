using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class Node : INode
    {
        protected Node()
        {
            Relations = new List<Relation>();
        }

        /// <summary>
        /// Collection of applicable outwards relations on the Node
        /// </summary>
        public ICollection<Relation> Relations { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check all required properites are in place
            return new[] { ValidationResult.Success };
        }
    }
}
