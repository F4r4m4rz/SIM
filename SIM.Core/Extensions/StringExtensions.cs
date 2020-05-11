using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ValidateNullOrWhitespace(this string @string, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                throw new ArgumentException("Value cannot be null or white space", parameterName);
            }

            return @string;
        }
    }
}
