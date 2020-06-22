using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Data
{
    public class ContextRepository
    {
        public ContextRepository()
        {

        }

        public IEnumerable<string> GetAll()
        {
            return Directory.GetFiles(@"C:\Users\ofsfabo1\AppData\Roaming\SIM\Json")
               .Select(a => Path.GetFileNameWithoutExtension(a).Split('.')[2]);
        }
    }
}
