using SIM.Core.Attributes;
using SIM.Core.Extensions;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public class DynamicIdentityProperty : DynamicProperty
    {
        public DynamicIdentityProperty()
        {

        }

        public DynamicIdentityProperty(string nameSpace, string propertyName, string targetNodeType)
        {
            try
            {
                Namespace = nameSpace.ValidateNullOrWhitespace(nameof(nameSpace));
                Name = propertyName.ValidateNullOrWhitespace(nameof(propertyName));
                TargetNodeType = targetNodeType.ValidateNullOrWhitespace(nameof(targetNodeType));
                Attributes.Add(new KeyValuePair<Type, object[]>(typeof(IdentityRelationTargetTypeAttribute),
                    new object[] { targetNodeType }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Type DerivedFrom => typeof(Relation);

        public string TargetNodeType { get; set; }
    }
}
