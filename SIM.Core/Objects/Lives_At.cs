using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    [RelationEndType(typeof(User), RelationEndEnum.Origin)]
    [RelationEndType(typeof(Location), RelationEndEnum.Target)]
    [SoloRelation(IsOriginSolo = true)]
    public sealed class Lives_At : Relation
    {
		public DateTime? Since
		{
			get
			{
				if (DateTime.TryParse(GetProperty(nameof(Since)).ToString(), out DateTime _since))
					return _since;
				return null;
			}

			set
			{
				SetProperty(nameof(Since), value);
			}
		}
	}
}
