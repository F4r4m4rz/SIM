using Microsoft.AspNetCore.Components;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SIM.Blazor.Components
{
    public class SimBooleanPropReprBase : ComponentBase
    {
        [Parameter]
        public ISimObject Owner { get; set; }

        [Parameter]
        public PropertyInfo BackingProprty { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        public bool Value
        {
            get
            {
                return (bool)BackingProprty.GetValue(Owner);
            }

            set
            {
                BackingProprty.SetValue(Owner, value);
            }
        }
    }
}
