using Microsoft.AspNetCore.Components;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Commands;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIM.Blazor.Components
{
    public class SimNewItemDialogBase : ComponentBase
    {
        [Parameter]
        public ISimCommand CreateCommand { get; set; }

        [Parameter]
        public ISimObject Owner { get; set; }

        [Parameter]
        public EventCallback CloseEventCallback { get; set; }

        [Inject]
        public ISimRepository Repository { get; set; }

        public ISimObject CreatedItem { get; private set; }

        public bool ShowDialog { get; set; } = false;
        
        public void Show()
        {
            ShowDialog = true;
            StateHasChanged();
        }

        private void FillCommand()
        {
            // Check if repository is required
            var x = CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(ISimRepository))).FirstOrDefault();
            CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(ISimRepository))).FirstOrDefault()?
                .SetValue(CreateCommand, Repository);

            // Check if Node is required
            CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(DynamicNode))).FirstOrDefault()
                .SetValue(CreateCommand, Owner as DynamicNode);
        }

        public async void Close()
        {
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(null);
            StateHasChanged();
        }

        protected void Create()
        {
            FillCommand();
            CreateCommand.Execute();
            Close();
        }
    }
}
