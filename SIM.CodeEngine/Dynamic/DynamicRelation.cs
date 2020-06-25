using Newtonsoft.Json;
using SIM.Core.Attributes;
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
    public class DynamicRelation : DynamicGraphObject
    {
        public DynamicRelation()
        {

        }

        public DynamicRelation(string nameSpace, string name, IEnumerable<string> originTypes, IEnumerable<string> targetTypes)
            : base(nameSpace, name)
        {
            try
            {
                AssignApplicableTypes(originTypes, "Origin");
                AssignApplicableTypes(targetTypes, "Target");
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        private void AssignApplicableTypes(IEnumerable<string> types, string end)
        {
            for (int i = 0; i < types.Count(); i++)
            {
                Attributes.Add(new KeyValuePair<Type, object[]>(typeof(RelationEndTypeAttribute),
                    new object[] { types.ElementAt(i), end }));
            }
        }

        public override string DerivedFrom => typeof(Relation).FullName;
    }
}
