using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SIM.Blazor.Admin.Data;
using SIM.CodeEngine.Commands;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Components
{
    public class OverviewBase : ComponentBase
    {
        [Inject]
        public ISimRepository repository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        protected IEnumerable<DynamicNode> Nodes => repository.GetAll().OfType<DynamicNode>();
        protected IEnumerable<DynamicRelation> Relations => repository.GetAll().OfType<DynamicRelation>();

        protected override Task OnInitializedAsync()
        {
            // Subscribe to mediator
            Mediator.ContextUpdated += Mediator_ContextUpdated;
            LoadRepository();

            return base.OnInitializedAsync();
        }

        private void LoadRepository()
        {
            Repository.objects.Clear();
            var com = new LoadRepositoryAsJson(repository, $"SIM.Aibel.{Mediator.Context}");
            try
            {
                com.Execute();
            }
            catch (FileNotFoundException)
            {

            }
        }

        private void Mediator_ContextUpdated(object sender, string e)
        {
            InvokeAsync(StateHasChanged);
            LoadRepository();
        }

        protected void NewNode()
        {
            NavigationManager.NavigateTo("/newitem/1");
        }

        protected void NewRelation()
        {
            NavigationManager.NavigateTo("/newitem/2");
        }
    }
}
