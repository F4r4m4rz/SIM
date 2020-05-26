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
    public class DynamicRelationProperty : DynamicProperty
    {
        public DynamicRelationProperty()
        {

        }

        public DynamicRelationProperty(string nameSpace, string propertyName, string targetNodeType, bool isRequired, bool isUserInput)
        {
            try
            {
                Namespace = nameSpace.ValidateNullOrWhitespace(nameof(nameSpace));
                Name = propertyName.ValidateNullOrWhitespace(nameof(propertyName));
                TargetNodeType = targetNodeType.ValidateNullOrWhitespace(nameof(targetNodeType));
                Attributes.Add(new KeyValuePair<Type, object[]>(typeof(PropertyRelationTargetTypeAttribute),
                    new object[] { targetNodeType }));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IsRequired = isRequired;
            IsUserInput = isUserInput;
        }

        public override Type DerivedFrom => typeof(Relation);

        public string TargetNodeType { get; set; }
    }
}
