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
        public static string String => typeof(string).FullName;
        public static string Integer => typeof(int).FullName;
        public static string Real => typeof(double).FullName;
        public static string Boolean => typeof(bool).FullName;
        public static string Collection => typeof(IList).FullName;
        public static string DateTime => typeof(DateTime).FullName;
    }
}
