using SIM.Core.Extensions;
using SIM.Core.Objects;
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
        public DynamicRelation(string nameSpace, string name, string originType, string targetType)
            : base(nameSpace, name)
        {
            try
            {
                OriginType = originType.ValidateNullOrWhitespace(nameof(originType));
                TargetType = targetType.ValidateNullOrWhitespace(nameof(targetType));
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public override object DerivedFrom => $"{typeof(Relation).Name}<{OriginType},{TargetType}>";
        public string OriginType { get; private set; }
        public string TargetType { get; private set; }
    }
}
