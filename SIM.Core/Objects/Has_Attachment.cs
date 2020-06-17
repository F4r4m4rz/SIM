using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    [RelationEndType(typeof(Attachment), RelationEndEnum.Target)]
    public sealed class Has_Attachment : Relation
    {
    }
}
