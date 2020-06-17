using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    [RelationEndType(typeof(Comment), RelationEndEnum.Origin)]
    [RelationEndType(typeof(User), RelationEndEnum.Target)]
    [SoloRelation(IsOriginSolo = true)]
    public sealed class Has_Authur : Relation
    {
    }
}
