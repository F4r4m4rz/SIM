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
        static DynamicPropertyType()
        {
            String = typeof(StringPropertyNode);
            Integer = typeof(IntegerPropertyNode);
            Real = typeof(DoublePropertyNode);
            Boolean = typeof(BooleanPropertyNode);
            DateTime = typeof(DateTimePropertyNode);
        }

        public static Type String { get; }
        public static Type Integer { get; }
        public static Type Real { get; }
        public static Type Boolean { get; }
        public static Type DateTime { get; }

        public static Type GetPropertyType(string type)
        {
            switch (type)
            {
                case "String":
                    return String;
                case "Integer":
                    return Integer;
                case "Real":
                    return Real;
                case "Boolean":
                    return Boolean;
                case "DateTime":
                    return DateTime;
                default:
                    throw new ArgumentException($"Could not recognize property node type of {type}");
            }
        }
    }
}
