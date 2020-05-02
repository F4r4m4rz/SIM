using SIM.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIM.Core.Abstraction
{
    public abstract class Node
    {
        protected Node()
        {
            Relations = new List<Relation>();
        }

        /// <summary>
        /// Property to indicate wether the Node requires unique ID
        /// </summary>
        internal bool HasUniqueId { get; set; }

        /// <summary>
        /// The pattern to be considered to generate unique ID (if required)
        /// </summary>
        internal string IdPattern { get; set; }

        /// <summary>
        /// Collection of applicable relations on the Node
        /// </summary>
        public ICollection<Relation> Relations { get; set; }
    }
}
