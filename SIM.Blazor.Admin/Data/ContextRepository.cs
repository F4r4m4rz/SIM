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
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"SIM\Json");
            return Directory.GetFiles(path)
               .Select(a => Path.GetFileNameWithoutExtension(a).Split('.')[2]);
        }
    }
}
