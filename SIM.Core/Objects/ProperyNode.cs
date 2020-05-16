﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class ProperyNode : ISimObject
    {
        protected object _value;

        public virtual void SetValue(object value)
        {
            _value = value;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
