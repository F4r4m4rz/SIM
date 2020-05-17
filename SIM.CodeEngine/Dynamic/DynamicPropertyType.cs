using SIM.Core.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Dynamic
{
    public static class DynamicPropertyType
    {
        public static string String => typeof(StringPropertyNode).FullName;
        public static string Integer => typeof(IntegerPropertyNode).FullName;
        public static string Real => typeof(DoublePropertyNode).FullName;
        public static string Boolean => typeof(BooleanPropertyNode).FullName;
        public static string Collection => typeof(IList).FullName;
        public static string DateTime => typeof(DateTimePropertyNode).FullName;
    }
}
