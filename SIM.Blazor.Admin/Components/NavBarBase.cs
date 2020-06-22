using Microsoft.AspNetCore.Components;
using SIM.Blazor.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace SIM.Blazor.Admin.Components
{
    public class NavBarBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ContextRepository ContextRepository { get; set; }

        protected void SetContext(string context)
        {
            Mediator.Context = context;
        }

        protected void NewContext()
        {
            Mediator.Context = "CAR";
        }
    }
}
