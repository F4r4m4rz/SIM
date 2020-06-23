using Microsoft.AspNetCore.Components;
using SIM.Blazor.Admin.Data;
using SIM.CodeEngine.Commands;
using SIM.CodeEngine.Dynamic;
using SIM.CodeEngine.Factory;
using SIM.Core.DataBase;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Components
{
    public class AddNewItemBase : ComponentBase
    {
        [Parameter]
        public Type TargetType { get; set; }

        [Inject]
        public ISimRepository Repository { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        public ISimObject Object { get; set; }

        public bool isSaved = false;

        protected override Task OnInitializedAsync()
        {
            Object = Activator.CreateInstance(TargetType) as ISimObject;
            return base.OnInitializedAsync();
        }

        protected void Submit()
        {
            Repository.Add(Object);
            isSaved = true;

            // Save json
            var cmd = new RepositoryAsJsonCommand(Repository, $"SIM.Aibel.{Mediator.Context}");
            cmd.Execute();
        }
    }
}
