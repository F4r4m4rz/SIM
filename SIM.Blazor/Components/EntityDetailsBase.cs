using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Components
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
        public ISimRepository DataService { get; set; }
    }
}
