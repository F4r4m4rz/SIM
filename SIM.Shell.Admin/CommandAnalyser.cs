using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell.Admin
{
    public class CommandAnalyser : IValidatableObject
    {
        private string userInput;

        public CommandAnalyser(string userInput)
        {
            this.userInput = userInput;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
        }
    }
}
