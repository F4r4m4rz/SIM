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
    public class DynamicObject : ISimObject
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

        public virtual Type DerivedFrom { get; set; }

        /// <summary>
        /// Namespace for the class which will be generated
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Name of type which will be created
        /// </summary>
        public string Name { get; set; }

        public ICollection<KeyValuePair<Type, object[]>> Attributes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
