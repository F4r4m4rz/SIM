using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIM.Core.Extensions;

namespace SIM.Core.Objects
{
    public class StringPropertyNode : GenericPropertyNode<string>
    {
        public StringPropertyNode(string value) : base(value.ValidateNullOrWhitespace(nameof(value)))
        {

        }
    }
}
