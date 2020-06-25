using Microsoft.AspNetCore.Components;
using SIM.CodeEngine.Commands;
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
    public enum ItemType { Attribute, Property }

    public class SimNewItemDialogBase : ComponentBase
    {
        [Parameter]
        public string Scope { get; set; }

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

        protected string AttributeName { get; set; }

        protected ICollection<string> AttributeProperties { get; set; } = new List<string>();

        protected Type AttributeType
        {
            get
            {
                Type t = null;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = assembly.GetTypes()
                        .Where(a => a.Name.Equals(AttributeName, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
                    if (t != null) break;
                }
                return t;
            }
        }
        
        public void Show()
        {
            Reset();
            ShowDialog = true;
            StateHasChanged();
        }

        private void Reset()
        {
            AttributeName = string.Empty;
            AttributeProperties.Clear();
        }

        private void FillCommand()
        {
            // Check if repository is required
            var x = CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(ISimRepository))).FirstOrDefault();
            CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(ISimRepository))).FirstOrDefault()?
                .SetValue(CreateCommand, Repository);

            // Check if Node is required
            CreateCommand.GetType().GetProperties().Where(a => a.PropertyType.Equals(typeof(DynamicGraphObject))).FirstOrDefault()
                .SetValue(CreateCommand, Owner as DynamicGraphObject);
        }

        public async void Close()
        {
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(null);
            StateHasChanged();
        }

        protected void Create()
        {
            if (Scope == "Properties")
            {
                CreateProperty();
            }
            else
            {
                CreateAttribute();
            }
            Close();
            // Update json
            var cmd = new RepositoryAsJsonCommand(Repository, (Owner as DynamicObject).Namespace);
            cmd.Execute();
        }

        private void CreateAttribute()
        {
            (Owner as DynamicObject).Attributes.Add(new KeyValuePair<Type, object[]>(AttributeType, AttributeProperties.ToArray()));
        }

        private void CreateProperty()
        {
            FillCommand();
            CreateCommand.Execute();
        }

        protected void AttributeNameAdded(ChangeEventArgs e)
        {
            AttributeName = e.Value as string;
            StateHasChanged();
        }

        protected void ValueEntered(ChangeEventArgs e)
        {
            AttributeProperties.Add(e.Value as string);
        }
    }
}
