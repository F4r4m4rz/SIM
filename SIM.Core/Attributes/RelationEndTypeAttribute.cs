using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    public enum RelationNode { Origin, Target }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RelationEndTypeAttribute : Attribute
    {
        public RelationEndTypeAttribute(string type, string node)
        {
            Type = type;
            Node = node;
        }

        public string Type { get; }
        public string Node { get; }
    }
}
