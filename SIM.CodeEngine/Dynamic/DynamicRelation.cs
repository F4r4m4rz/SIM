using SIM.Core.Abstractions;
using SIM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicRelation : DynamicObject
    {
        private readonly string derivedFrom;

        public DynamicRelation(string nameSpace, string name, string originType, string targetType)
            : base(nameSpace, name)
        {
            try
            {
                this.derivedFrom = string.Format("Relation<{0},{1}>",
                    originType.ValidateNullOrWhitespace(nameof(originType)),
                    targetType.ValidateNullOrWhitespace(nameof(targetType)));
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public override object DerivedFrom => derivedFrom;
    }
}
