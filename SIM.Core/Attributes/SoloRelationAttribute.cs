using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class SoloRelationAttribute : Attribute
    {
        /// <summary>
        /// If the origin Node can accept only one of this relation
        /// </summary>
        public bool IsOriginSolo { get; set; } = false;

        /// <summary>
        /// If the target Node can accept only one of this relation
        /// </summary>
        public bool IsTargetSolo { get; set; } = false;
    }
}
