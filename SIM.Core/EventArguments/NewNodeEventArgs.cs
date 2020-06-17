using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.EventArguments
{
    public class NewNodeEventArgs : EventArgs
    {
        public DateTime CreatedOn { get; set; }
    }
}
