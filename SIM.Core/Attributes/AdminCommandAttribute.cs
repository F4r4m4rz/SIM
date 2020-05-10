using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AdminCommandAttribute : Attribute
    {
    }
}
