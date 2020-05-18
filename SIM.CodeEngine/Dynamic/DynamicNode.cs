using Newtonsoft.Json;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicNode : DynamicObject
    {
        public DynamicNode(string nameSpace, string name) : base(nameSpace, name)
        {
            Properties = new List<DynamicProperty>();
        }

        public override Type DerivedFrom => typeof(Node);

        [JsonProperty(Order = 5)]
        public ICollection<DynamicProperty> Properties { get; set; }
    }
}
