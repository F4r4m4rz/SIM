﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class SimNodeFactory
    {
        public virtual T New<T>(SimNodeConstructionArgument<T> constructionArgument) where T: Node, new()
        {
            var obj = new T();
            constructionArgument.Populate(obj);
            return obj;
        }

        public virtual SimNodeConstructionArgument<T> GetConstructionArguments<T>() where T : Node, new()
        {
            return new SimNodeConstructionArgument<T>();
        }
    }
}