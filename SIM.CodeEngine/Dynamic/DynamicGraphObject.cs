using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public abstract class DynamicGraphObject : DynamicObject
    {
        protected DynamicGraphObject()
        {
            Properties = new List<DynamicProperty>();
        }
        public DynamicGraphObject(string nameSpace, string name) : base(nameSpace, name)
        {
            Properties = new List<DynamicProperty>();
        }

        [JsonProperty(Order = 5)]
        public ICollection<DynamicProperty> Properties { get; set; }
    }
}
