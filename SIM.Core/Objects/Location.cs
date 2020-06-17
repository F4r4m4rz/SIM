using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public sealed class Location : Node
    {
		public string Country
		{
			get
			{
				return GetProperty(nameof(Country)) as string;
			}

			set
			{
				SetProperty(nameof(Country), value);
			}
		}

		public string Provience
		{
			get
			{
				return GetProperty(nameof(Provience)) as string;
			}

			set
			{
				SetProperty(nameof(Provience), value);
			}
		}

		public string City
		{
			get
			{
				return GetProperty(nameof(City)) as string;
			}

			set
			{
				SetProperty(nameof(City), value);
			}
		}

		public string Address
		{
			get
			{
				return GetProperty(nameof(Address)) as string;
			}

			set
			{
				SetProperty(nameof(Address), value);
			}
		}

		public double Langtitude
		{
			get
			{
				if (double.TryParse(GetProperty(nameof(Langtitude)).ToString(), out double _langtitude))
					return _langtitude;
				return double.NaN;
			}

			set
			{
				SetProperty(nameof(Langtitude), value);
			}
		}

		public double Latitude
		{
			get
			{
				if (double.TryParse(GetProperty(nameof(Latitude)).ToString(), out double _latitude))
					return _latitude;
				return double.NaN;
			}

			set
			{
				SetProperty(nameof(Latitude), value);
			}
		}
	}
}
