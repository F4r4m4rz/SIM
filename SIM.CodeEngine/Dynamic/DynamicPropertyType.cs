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

        public static string GetPropertyType(string type, ISimRepository simRepository)
        {
            string typeFromCore = GetPropertyTypeFromCore(type, simRepository);
            if (typeFromCore != null) return typeFromCore;

            switch (type)
            {
                case "String":
                    return String.FullName;
                case "Integer":
                    return Integer.FullName;
                case "Double":
                    return Double.FullName;
                case "Boolean":
                    return Boolean.FullName;
                case "DateTime":
                    return DateTime.FullName;
                default:
                    throw new ArgumentException($"Could not recognize property node type of {type}");
            }
        }

        private static string GetPropertyTypeFromCore(string type, ISimRepository simRepository)
        {
            System.Reflection.Assembly core = System.Reflection.Assembly.GetAssembly(typeof(Relation));
            Type typeFromCore = core.GetType(type);

            if (typeFromCore != null && typeFromCore.BaseType.Equals(typeof(Relation)))
                return typeFromCore.FullName;
            else
            {
                ISimObject typeFromCurrentRepos = simRepository.Get(a => a is DynamicRelation && (a as DynamicRelation).Name.Equals(type, StringComparison.OrdinalIgnoreCase));
                if (typeFromCurrentRepos != null)
                    return string.Format("{0}.{1}", (typeFromCurrentRepos as DynamicRelation).Namespace, (typeFromCurrentRepos as DynamicRelation).Name);
            }

            return null;
        }

        public static void GetAllValidTypes(ISimRepository simRepository, out IEnumerable<string> regularTypes, out IEnumerable<string> nodeTypes)
        {
            regularTypes = GetAllValidRegularTypes();
            nodeTypes = GetAllValidNodeTypes(simRepository);
        }

        private static IEnumerable<string> GetAllValidNodeTypes(ISimRepository simRepository)
        {
            var result = new List<string>();

            // From Core
            System.Reflection.Assembly core = System.Reflection.Assembly.GetAssembly(typeof(Relation));
            result.AddRange(core.GetTypes()
                .Where(a => a.Name.Equals("Node", StringComparison.OrdinalIgnoreCase))
                .Select(a=>a.Name));

            // From Repository
            result.AddRange(simRepository
                .GetAll(a => a is DynamicRelation)
                .Select(a=>(a as DynamicRelation).Name));

            return result;
        }

        private static IEnumerable<string> GetAllValidRegularTypes()
        {
            var result = new List<string>();
            result.Add("String");
            result.Add("Double");
            result.Add("Integer");
            result.Add("Boolean");
            result.Add("DateTime");
            return result;
        }
    }
}
