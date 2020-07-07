using Microsoft.AspNetCore.Components;
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
        private static Assembly assembly;
        public event EventHandler NavbarRefreshHandler;
        public event EventHandler IndexRefreshHandler;

        public IEnumerable<Type> Entities => ReadNodeEntities();

        internal Assembly LoadAssembly()
        {
            var assemblyBytes = File.ReadAllBytes($@"C:\Users\ofsfabo1\AppData\Roaming\SIM\Auto generated assemblies\SIM.Aibel.{Context}.dll");
            assembly = AppDomain.CurrentDomain.Load(assemblyBytes);
            return assembly;
        }

        private IEnumerable<Type> ReadNodeEntities()
        {
            return assembly.GetTypes()
                .Where(a => a.BaseType == typeof(Node) && a.GetCustomAttribute(typeof(VisibleNodeAttribute)) != null);
        }

        public void RefreshIndexPage(object requester)
        {
            IndexRefreshHandler?.Invoke(requester, null);
        }

        public void RefreshNavbar(object requester)
        {
            NavbarRefreshHandler?.Invoke(requester, null);
        }
    }
}
