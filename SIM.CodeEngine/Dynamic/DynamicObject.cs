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
    [JsonConverter(typeof(DynamicObjectJsonConverter))]
    public abstract class DynamicObject : ISimObject
    {
        protected DynamicObject()
        {
            Attributes = new List<KeyValuePair<Type, object[]>>();
        }

        protected DynamicObject(string nameSpace, string name) : this()
        {
            try
            {
                Namespace = nameSpace.ValidateNullOrWhitespace(nameof(nameSpace));
                Name = name.ValidateNullOrWhitespace(nameof(name));
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// Namespace for the class which will be generated
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Namespace { get; set; }

        /// <summary>
        /// Name of type which will be created
        /// </summary>
        [JsonProperty(Order = 2)]
        public string Name { get; set; }

        [JsonProperty(Order = 3)]
        public virtual Type DerivedFrom { get; set; }

        [JsonProperty(Order = 4)]
        public ICollection<KeyValuePair<Type, object[]>> Attributes { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
