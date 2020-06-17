using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public sealed class Attachment : Node
    {
		[Required]
		public string Name
		{
			get
			{
				return GetProperty(nameof(Name)) as string;
			}

			set
			{
				SetProperty(nameof(Name), value);
			}
		}

		[Required]
		[MaxLength(200, ErrorMessage = "Cannot accept comments longer than 500 character")]
		public string Description
		{
			get
			{
				return GetProperty(nameof(Description)) as string;
			}

			set
			{
				SetProperty(nameof(Description), value);
			}
		}

		[Required]
		public string Path
		{
			get
			{
				return GetProperty(nameof(Path)) as string;
			}

			set
			{
				SetProperty(nameof(Path), value);
			}
		}

		[Required]
		public string Extension
		{
			get
			{
				return GetProperty(nameof(Extension)) as string;
			}

			set
			{
				SetProperty(nameof(Extension), value);
			}
		}
	}
}
