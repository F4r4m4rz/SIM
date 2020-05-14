using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class Relation
    {
        protected Relation()
        {

        }

        /// <summary>
        /// If the origin Node can have only one of this relation
        /// </summary>
        internal bool IsOriginSolo { get; set; }

        /// <summary>
        /// If the destination Node can accept only one of this relation
        /// </summary>
        internal bool IsDestinationSolo { get; set; }

        /// <summary>
        /// The Node which lays at the start of the relation arrow
        /// </summary>
        public Node Origin { get; set; }

        /// <summary>
        /// The Node which lays at the end of the relation arrow
        /// </summary>
        public Node Target { get; set; }
    }
}
