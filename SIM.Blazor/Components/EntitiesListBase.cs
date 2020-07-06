using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Components
{
    public class EntitiesListBase : ComponentBase
    {
        [Inject]
        public Mediator Mediator { get; set; }

        protected IEnumerable<Type> Entities { get; set; }

        protected override Task OnInitializedAsync()
        {
            Entities = Mediator.ReadNodeEntities();
            return base.OnInitializedAsync();
        }
    }
}
