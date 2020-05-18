using SIM.Core.Objects;
using SIM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicProperty : DynamicObject
    {
        public DynamicProperty()
        {

        }

        public DynamicProperty(string nameSpace, string propertyName, string valueType, bool isRequired, bool isUserInput)
        {
            try
            {
                Namespace = nameSpace.ValidateNullOrWhitespace(nameof(nameSpace));
                Name = propertyName.ValidateNullOrWhitespace(nameof(propertyName));
                ValueType = DynamicPropertyType.GetPropertyType(valueType);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            IsRequired = isRequired;
            IsUserInput = isUserInput;
        }

        public override Type DerivedFrom { get => typeof(PropertyRelation); }

        [JsonProperty(Order = 5)]
        public Type ValueType { get; set; }

        /// <summary>
        /// If the propery can be null
        /// </summary>
        [JsonProperty(Order = 6)]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Indicates whether the user should define the valu
        /// </summary>
        [JsonProperty(Order = 7)]
        public bool IsUserInput { get; set; }

        /// <summary>
        /// The command to be executed upon assignment
        /// </summary>
        [JsonProperty(Order = 8)]
        public string AssignmentCommand { get; set; }

        /// <summary>
        /// Default value of the property upon intialization
        /// </summary>
        [JsonProperty(Order = 9)]
        public object DefaultValue { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
