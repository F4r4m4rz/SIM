using Microsoft.AspNetCore.Components;
using SIM.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIM.Blazor.Components
{
    public class SimNewItemDialogBase : ComponentBase
    {
        [Parameter]
        public ISimCommand CreateCommand { get; set; }

        public object CreatedItem { get; private set; }

        public bool ShowDialog { get; set; } = false;
        
        public void Show()
        {
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
