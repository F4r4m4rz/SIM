using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Abstractions
{
    public abstract class Relation
    {
        /// <summary>
        /// If the origin Node can have only one of this relation
        /// </summary>
        internal bool IsOriginSolo { get; set; }

        /// <summary>
        /// If the destination Node can accept only one of this relation
        /// </summary>
        internal bool IsDestinationSolo { get; set; }
    }
}
