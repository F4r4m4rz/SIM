using SIM.Core.Attributes;
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
            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// The Node which lays at the start of the relation arrow
        /// </summary>
        public Node Origin { get; set; }

        /// <summary>
        /// The Node which lays at the end of the relation arrow
        /// </summary>
        public Node Target { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            // Check if the Origin and Target are of the correct type
            // Attribute indicating Origin types
            var isOriginValid = GetType().GetCustomAttributes(typeof(RelationEndTypeAttribute), true)
                .Where(a => (a as RelationEndTypeAttribute).End == RelationEndEnum.Origin && (a as RelationEndTypeAttribute).Type == Origin.GetType())
                .FirstOrDefault() != null;

            if (!isOriginValid)
                result.Add(new ValidationResult($"Origin type not valid: {Origin.GetType()}"));

            // Attribute indicating Target types
            var isTargetValid = GetType().GetCustomAttributes(typeof(RelationEndTypeAttribute), true)
                .Where(a => (a as RelationEndTypeAttribute).End == RelationEndEnum.Target && (a as RelationEndTypeAttribute).Type == Target.GetType())
                .FirstOrDefault() != null;

            if (!isTargetValid)
                result.Add(new ValidationResult($"Target type not valid: {Target.GetType()}"));

            // TODO: Check if relation is OriginSolo or TargetSolo and rule is respected

            if (result.Count == 0)
                return new ValidationResult[] { ValidationResult.Success };

            return result;
        }

        protected virtual object GetProperty(string key)
        {
            if (Properties.ContainsKey(nameof(key)))
            {
                return Properties[nameof(key)] as string;
            }
            else
                return null;
        }

        protected virtual void SetProperty(string key, object value)
        {
            Properties[key] = value;
        }
    }
}
