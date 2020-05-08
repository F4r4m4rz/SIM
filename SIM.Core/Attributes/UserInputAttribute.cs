using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class UserInputAttribute : Attribute
    {
    }
}
