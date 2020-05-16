﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class GenericPropertyNode<T> : ProperyNode
    {
        public T Value => (T)_value;
    }
}