using Neo4j.Driver;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INode = Neo4j.Driver.INode;

namespace SIM.Neo4j
{
    public static class Convertor
    {
        public static ISimObject Convert(INode node)
        {
            var typeStr = node.Labels[0];
            var asseembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name.Contains("SIM."));
            List<Type> types = new List<Type>();
            asseembly.ToList().ForEach(a => types.AddRange(a.GetTypes()));
            var type = types.First(a => a.Name.Contains(typeStr));
            var obj = Activator.CreateInstance(type);
            foreach (var prop in node.Properties)
            {
                type.GetProperty(prop.Key).SetValue(obj, prop.Value);
            }
            return obj as ISimObject;
        }
    }
}
