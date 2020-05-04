using SIM.Core.Abstraction;
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
            Relations = new List<DynamicRelation>();
        }

        public override Type DerivedFrom => typeof(Node);

        public ICollection<DynamicRelation> Relations { get; set; }
    }
}
