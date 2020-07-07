using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Pages
{
    public class IndexBase : ComponentBase
    {
        public bool ShowContextList
        {
            get
            {
                return string.IsNullOrWhiteSpace(SIM.Blazor.Data.Mediator.Context);
            }
        }

        [Inject]
        public Mediator Mediator { get; set; }

        protected override Task OnInitializedAsync()
        {
            Mediator.IndexRefreshHandler += new EventHandler(RefreshRequested);
            return base.OnInitializedAsync();
        }

        private void RefreshRequested(object sender, EventArgs e)
        {
            InvokeAsync(() => StateHasChanged());
        }
    }
}
