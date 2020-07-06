using SIM.Core.Attributes;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SIM.Blazor.Data
{
    public class Mediator
    {
        public static string Context { get; set; }

        internal Assembly LoadAssembly()
        {
            var assemblyBytes = File.ReadAllBytes($@"C:\Users\ofsfabo1\AppData\Roaming\SIM\Auto generated assemblies\SIM.Aibel.{Context}.dll");
            return AppDomain.CurrentDomain.Load(assemblyBytes);
        }

        internal IEnumerable<Type> ReadNodeEntities()
        {
            Assembly assembly = LoadAssembly();
            return assembly.GetTypes()
                .Where(a => a.BaseType == typeof(Node) && a.GetCustomAttribute(typeof(VisibleNodeAttribute)) != null);
        }
    }
}
