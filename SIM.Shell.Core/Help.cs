using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIM.Shell.Core
{
    public class Help
    {
        public Help()
        {

        }

        public IEnumerable<string> GetOptions()
        {
            // Get all types in SIM.CodeEngine assembly
            return Assembly.Load("SIM.CodeEngine.dll")
                .GetTypes().Where(c=>!c.IsAbstract && c.IsClass && c.IsPublic)
                .Select(c => c.Name);
        }
    }
}
