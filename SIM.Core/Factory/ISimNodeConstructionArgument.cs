﻿using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIM.Core.Factory
{
    public interface ISimNodeConstructionArgument
    {
        Node Object { get; }
        Type ObjectType { get; }
        PropertyInfo[] Arguments { get; }
        IRelation[] ArgumentValues { get; }
        PropertyInfo[] GetConstructionArguments();
        IEnumerable<Type> GetExpectedTypes();
        void AssignArgumentValues(params INode[] values);
    }
}