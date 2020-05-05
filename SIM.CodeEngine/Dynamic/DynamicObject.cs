using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public abstract class DynamicObject
    {
        protected DynamicObject()
        {
            Properties = new List<DynamicProperty>();
        }

        public abstract object DerivedFrom { get; }

        /// <summary>
        /// Namespace for the class which will be generated
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Name of type which will be created
        /// </summary>
        public string Name { get; set; }

        public ICollection<DynamicProperty> Properties { get; set; }
    }
}
