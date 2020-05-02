using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicProperty
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Type of the property (To be defined from DynamicPropertyType class)
        /// </summary>
        public string PropertyType { get; set; }

        /// <summary>
        /// If the propery can be null
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Default value of the property upon intialization
        /// </summary>
        public object DefaultValue { get; set; }
    }
}
