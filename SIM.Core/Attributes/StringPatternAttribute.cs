using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class StringPatternAttribute : ValidationAttribute
    {
        public StringPatternAttribute(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException($"{nameof(Pattern)} cannot be null, empty or white space", nameof(pattern));
            }

            Pattern = pattern;
        }
        public string Pattern { get; }
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value as string, Pattern);
        }
    }
}
