using SIM.Core.Abstractions;
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

        public DynamicRelation(string derivedFrom)
        {
            if (string.IsNullOrEmpty(derivedFrom))
            {
                throw new ArgumentException("Cannot accept null or white space", nameof(derivedFrom));
            }

            this.derivedFrom = derivedFrom;
        }

        public override object DerivedFrom => derivedFrom;
    }
}
