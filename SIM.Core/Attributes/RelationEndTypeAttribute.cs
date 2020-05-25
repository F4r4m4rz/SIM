﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    public enum RelationNode { Origin, Target }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RelationEndTypeAttribute : Attribute
    {
        public RelationEndTypeAttribute(Type type, string node)
        {
            Type = type;
            Node = node;
        }

        public Type Type { get; }
        public string Node { get; }
    }
}
