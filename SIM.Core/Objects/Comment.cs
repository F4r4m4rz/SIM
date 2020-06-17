using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public sealed class Comment : Node
    {
        [Required]
        public Has_Authur Has_Authur { get; set; }

		[Required]
		public DateTime? TimeStamp
		{
			get
			{
				if (DateTime.TryParse(GetProperty(nameof(TimeStamp)).ToString(), out DateTime _since))
					return _since;
				return null;
			}

			set
			{
				SetProperty(nameof(TimeStamp), value);
			}
		}

		public string Subject
		{
			get
			{
				return GetProperty(nameof(Subject)) as string;
			}

			set
			{
				SetProperty(nameof(Subject), value);
			}
		}

		[Required]
		[MaxLength(500, ErrorMessage = "Cannot accept comments longer than 500 character")]
		public string Body
		{
			get
			{
				return GetProperty(nameof(Body)) as string;
			}

			set
			{
				SetProperty(nameof(Body), value);
			}
		}
	}
}
