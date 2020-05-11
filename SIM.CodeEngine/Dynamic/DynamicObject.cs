using SIM.Core.Abstractions;
using SIM.Core.Extensions;
using SIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public abstract class DynamicObject : ISimObject
    {
        protected DynamicObject()
        {
            Properties = new List<DynamicProperty>();
        }

        protected DynamicObject(string nameSpace, string name)
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

        public abstract object DerivedFrom { get; }

        /// <summary>
        /// Namespace for the class which will be generated
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// Name of type which will be created
        /// </summary>
        public string Name { get; }

        public ICollection<DynamicProperty> Properties { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
