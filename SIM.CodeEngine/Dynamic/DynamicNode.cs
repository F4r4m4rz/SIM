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
    public class DynamicNode : DynamicGraphObject
    {
        public DynamicNode()
        {

        }

        public DynamicNode(string nameSpace, string name, bool isVisible) : base(nameSpace, name)
        {
            if (isVisible)
                Attributes.Add(new KeyValuePair<Type, object[]>(typeof(VisibleNodeAttribute), new object[0]));
        }

        public override string DerivedFrom => typeof(Node).FullName;

        [JsonProperty(Order = 6)]
        public bool IsVisible { get; set; }
    }
}
