using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using SIM.Core.Objects;
using SIM.DataBase;
using SIM.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.InternalComponents
{
    public class EntityDetailsBase : ComponentBase
    {
        [Parameter]
        public string TypeName { get; set; }

        public Type Type
        {
            get
            {
                return Mediator.GetTypeByName(TypeName);
            }
        }

        [Inject]
        public Mediator Mediator { get; set; }

        [Inject]
        public Neo4jRepository DataService { get; set; }

        public IEnumerable<ISimObject> Objects { get; set; } = new List<ISimObject>();

        protected override async Task OnParametersSetAsync()
        {
            Objects = await DataService.GetAll(TypeName);
            await base.OnParametersSetAsync();
        }
    }
}
