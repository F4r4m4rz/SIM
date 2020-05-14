using SIM.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            SIM.Aibel.JSB.CO co = new Aibel.JSB.CO();
            SIM.Aibel.JSB.SDI sdi = new Aibel.JSB.SDI();
            var com = new CurrentTimeCommand(sdi, sdi.GetType().GetProperty("StartDate"), DateTime.Now);
            com.Execute();
            co.Aveva_Id = "=1332465/6546";
            var rel = new SIM.Aibel.JSB.HasSDI()
            {
                Origin = co,
                Target = sdi
            };
            co.Relations.Add(rel);
        }
    }
}
