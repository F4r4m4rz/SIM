using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public sealed class User : Node
    {
		/// <summary>
		/// User id
		/// </summary>
		[Required]
		public string UserId
		{
			get
			{
				return GetProperty(nameof(UserId)) as string;
			}

			set
			{
				SetProperty(nameof(UserId), value);
			}
		}

		[Required]
		public string FirstName
		{
			get
			{
				return GetProperty(nameof(FirstName)) as string;
			}

			set
			{
				SetProperty(nameof(FirstName), value);
			}
		}

		public string MiddleName
		{
			get
			{
				return GetProperty(nameof(MiddleName)) as string;
			}

			set
			{
				SetProperty(nameof(MiddleName), value);
			}
		}

		[Required]
		public string LastName
		{
			get
			{
				return GetProperty(nameof(LastName)) as string;
			}

			set
			{
				SetProperty(nameof(LastName), value);
			}
		}

		public string FullName
		{
			get
			{
				return string.IsNullOrWhiteSpace(MiddleName) ?
					string.Format("{0} {1}", FirstName, LastName) :
					string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
			}
		}

		[Required]
		public string Email
		{
			get
			{
				return GetProperty(nameof(Email)) as string;
			}

			set
			{
				SetProperty(nameof(Email), value);
			}
		}

		public Lives_At lives_At { get; set; }
	}
}
