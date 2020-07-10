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

        public Graph NewEntityGraph { get; set; } = new Graph();

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
            NewEntityGraph.Nodes.Add(NewEntity as Node);
            return base.OnInitializedAsync();
        }

        public async Task Create()
        {
            for (int i = 0; i < ConstructionArgument.Arguments.Length; i++)
            {
                var prop = ConstructionArgument.Arguments[i];
                object propVal = null;
                try
                {
                    propVal = prop.GetValue(NewEntity);
                }
                catch
                {
                }

                if (propVal != null)
                {
                    //ConstructionArgument.ArgumentValues[i] = propVal;
                }
                else if (prop.Name == "Issued_By")
                {
                    var type = Mediator.GetTypeByName(prop.Name);
                    var relType = (await DataService.GetAll("User")).FirstOrDefault() as Node;
                    var rel = (NewEntity as Node).RelateTo(type, relType, true);
                    rel.Properties["On"] = DateTime.Now;
                    NewEntityGraph.Relations.Add(rel);
                }
                else if (prop.Name == "DataCode")
                {
                    prop.SetValue(NewEntity, "SDI_1");
                }
                else if (prop.Name == "Revision")
                {
                    prop.SetValue(NewEntity, 1);
                }
            }

            DataService.Add(NewEntityGraph);
        }
    }
}
