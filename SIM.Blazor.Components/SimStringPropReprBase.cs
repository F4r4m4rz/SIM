﻿using Microsoft.AspNetCore.Components;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIM.Blazor.Components
{
    public class SimStringPropReprBase : ComponentBase
    {
        [Parameter]
        public object Owner { get; set; }

        [Parameter]
        public PropertyInfo BackingProprty { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        public string Value
        {
            get
            {
                return BackingProprty.GetValue(Owner)?.ToString();
            }

            set
            {
                BackingProprty.SetValue(Owner, value);
            }
        }
    }
}
