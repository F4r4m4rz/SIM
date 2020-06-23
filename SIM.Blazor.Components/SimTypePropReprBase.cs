using Microsoft.AspNetCore.Components;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIM.Blazor.Components
{
    public class SimTypePropReprBase : ComponentBase
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
                Type _value = Type.GetType(value);
                BackingProprty.SetValue(Owner, _value);
            }
        }
    }
}
