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
                return Directory.GetFiles(@"C:\Users\ofsfabo1\AppData\Roaming\SIM\Auto generated assemblies", "SIM.Aibel.*.dll")
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
