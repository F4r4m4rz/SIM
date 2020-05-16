using SIM.Core.Objects;
using SIM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicProperty : DynamicObject
    {
        public DynamicProperty()
        {

        }

        public DynamicProperty(string nameSpace, string propertyName)
        {
            try
            {
                Name = propertyName.ValidateNullOrWhitespace(nameof(propertyName));
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public override Type DerivedFrom { get => typeof(PropertyRelation); }

        /// <summary>
        /// If the propery can be null
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Indicates whether the user should define the valu
        /// </summary>
        public bool IsUserInput { get; set; }

        /// <summary>
        /// The state on which the status shall be set
        /// </summary>
        public string RequiredOnState { get; set; }

        /// <summary>
        /// The command to be executed upon assignment
        /// </summary>
        public string AssignmentCommand { get; set; }

        /// <summary>
        /// Default value of the property upon intialization
        /// </summary>
        public object DefaultValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Propery cannot have RequiredOnState not null and IsNullable true
            if (IsNullable && !RequiredOnState.Equals("CREATE", StringComparison.OrdinalIgnoreCase))
                return new[] { new ValidationResult("Propery cannot be nullable and have requiredOnState Value other than 'CREATE' ") };

            return new[] { ValidationResult.Success };
        }
    }
}
