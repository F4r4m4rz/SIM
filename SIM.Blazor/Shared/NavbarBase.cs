using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Shared
{
    public class NavbarBase : ComponentBase
    {
        protected bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        [Inject]
        public Mediator Mediator { get; set; }

        protected bool IsContextSet
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Mediator.Context);
            }
        }

        protected override Task OnInitializedAsync()
        {
            Mediator.NavbarRefreshHandler += new EventHandler(RefreshRequested);
            return base.OnInitializedAsync();
        }

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void RefreshRequested(object sender, EventArgs e)
        {
            InvokeAsync(() => StateHasChanged());
        }
    }
}
