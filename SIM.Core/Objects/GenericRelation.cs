using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class Relation<TOrigin, TTarget> 
        : Relation where TOrigin: Node where TTarget: Node
    {
        /// <summary>
        /// The Node which lays at the start of the relation arrow
        /// </summary>
        public TOrigin Origin { get; internal set; }

        /// <summary>
        /// The Node which lays at the end of the relation arrow
        /// </summary>
        public TTarget Target { get; internal set; }
    }
}
