using SIM.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicRelation : DynamicObject
    {
        public override Type DerivedFrom => typeof(Relation);
    }
}
