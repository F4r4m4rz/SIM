using SIM.Core.Objects;
using SIM.DataBase;
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
            String = typeof(string);
            Integer = typeof(int);
            Double = typeof(double);
            Boolean = typeof(bool);
            DateTime = typeof(DateTime);
        }

        public static Type String { get; }
        public static Type Integer { get; }
        public static Type Double { get; }
        public static Type Boolean { get; }
        public static Type DateTime { get; }

        public static Type GetPropertyType(string type, ISimRepository simRepository)
        {
            Type typeFromCore = GetPropertyTypeFromCore(type, simRepository);
            if (typeFromCore != null) return typeFromCore;

            switch (type)
            {
                case "String":
                    return String;
                case "Integer":
                    return Integer;
                case "Double":
                    return Double;
                case "Boolean":
                    return Boolean;
                case "DateTime":
                    return DateTime;
                default:
                    throw new ArgumentException($"Could not recognize property node type of {type}");
            }
        }

        private static Type GetPropertyTypeFromCore(string type, ISimRepository simRepository)
        {
            System.Reflection.Assembly core = System.Reflection.Assembly.GetAssembly(typeof(Relation));
            Type typeFromCore = core.GetType(type);

            if (typeFromCore != null && typeFromCore.BaseType.Equals(typeof(Relation)))
                return typeFromCore;
            else
            {
                Type typeFromCurrentRepos = simRepository.Get(a => a.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase))?.GetType();
                if (typeFromCurrentRepos != null && typeFromCurrentRepos.BaseType.Equals(typeof(Relation)))
                    return typeFromCurrentRepos;
            }

            return null;
        }
    }
}
