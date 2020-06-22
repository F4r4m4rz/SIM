using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Components
{
    public class ParameterValue
    {
        public string _stringArrayValue;
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
        public object ObjectValue { get; set; }
        public string[] StringArrayValue
        {
            get
            {
                if (_stringArrayValue == null) return null;
                return _stringArrayValue.Split(' ');
            }
        }
    }
}
