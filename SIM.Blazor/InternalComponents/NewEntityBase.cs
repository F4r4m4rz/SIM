using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using SIM.Core.Attributes;
using SIM.Core.Factory;
using SIM.Core.Objects;
using SIM.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SIM.Blazor.InternalComponents
{
    public class NewEntityBase : ComponentBase
    {
        [Parameter]
        public Type EntityType { get; set; }

        [Inject]
        public SimNodeFactory NodeFactory { get; set; }

        [Inject]
        public Mediator Mediator { get; set; }

        [Inject]
        public Neo4jRepository DataService { get; set; }

        public ISimObject NewEntity { get; set; }

        public ISimNodeConstructionArgument ConstructionArgument { get; set; }

        public IEnumerable<PropertyInfo> UserProperties
        {
            get
            {
                return ConstructionArgument.Arguments
                    .Where(a => a.GetCustomAttribute(typeof(UserInputAttribute)) != null);
            }
        }

        private ISimNodeConstructionArgument GetConstructionArgument()
        {
            return NodeFactory.GetConstructionArguments(EntityType);
        }

        protected override Task OnInitializedAsync()
        {
            ConstructionArgument = GetConstructionArgument();
            NewEntity = NodeFactory.New(ConstructionArgument);
            return base.OnInitializedAsync();
        }

        public void Create()
        {
            for (int i = 0; i < ConstructionArgument.Arguments.Length; i++)
            {
                var prop = ConstructionArgument.Arguments[i];
                var propVal = prop.GetValue(NewEntity);
                if (propVal != null)
                {
                    ConstructionArgument.ArgumentValues[i] = propVal;
                }
                else if (prop.Name == "Issued_By")
                {
                    var type = Mediator.GetTypeByName(prop.Name);
                }
                else if (prop.Name == "DataCode")
                {

                }
                else if (prop.Name == "Revision")
                {

                }
            }
        }
    }
}
