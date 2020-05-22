﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class BooleanPropertyNode : GenericPropertyNode<bool>
    {
        public BooleanPropertyNode(bool value)
        {
            SetValue(value);
        }
    }
}
