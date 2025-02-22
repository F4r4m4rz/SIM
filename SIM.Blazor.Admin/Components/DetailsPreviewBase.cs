﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SIM.CodeEngine.Dynamic;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Components
{
    public class DetailsPreviewBase : ComponentBase
    {
        [Inject]
        public ISimRepository Repository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public DynamicObject Object { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
    }
}
