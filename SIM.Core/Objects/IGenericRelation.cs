using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public interface IGenericRelation<TOrigin, TTarget> : IRelation where TOrigin : ISimObject 
                                                                    where TTarget : ISimObject
    {
        TOrigin Origin { get; }
        TTarget Target { get; }
    }
}
