using Newtonsoft.Json;
using SIM.Core.Attributes;
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
        public DynamicNode()
        {

        }

        public DynamicNode(string nameSpace, string name, bool isVisible) : base(nameSpace, name)
        {
            Properties = new List<DynamicProperty>();
            if (isVisible)
                Attributes.Add(new KeyValuePair<Type, object[]>(typeof(VisibleNodeAttribute), new object[0]));
        }

        public override Type DerivedFrom => typeof(Node);

        [JsonProperty(Order = 5)]
        public ICollection<DynamicProperty> Properties { get; set; }

        [JsonProperty(Order = 6)]
        public bool IsVisible { get; set; }
    }
}
