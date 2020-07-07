using Microsoft.AspNetCore.Components;
using SIM.Blazor.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Components
{
    public class ContextListBase : ComponentBase
    {
        public IEnumerable<string> AvailableContexts
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"SIM\Auto generated assemblies");
                return Directory.GetFiles(path, "SIM.Aibel.*.dll")
                    .Select(a => a.Split('.')[2]);
            }
        }

        [Inject]
        public Mediator Mediator { get; set; }

        protected void Context_OnClick(string context)
        {
            Mediator.Context = context;
            Mediator.LoadAssembly();
            Mediator.RefreshIndexPage(this);
            Mediator.RefreshNavbar(this);
        }
    }
}
