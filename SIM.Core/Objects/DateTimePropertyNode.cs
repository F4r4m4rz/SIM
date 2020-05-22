using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class DateTimePropertyNode : GenericPropertyNode<DateTime>
    {
        public DateTimePropertyNode(DateTime dateTime)
        {
            SetValue(dateTime);
        }
    }
}
