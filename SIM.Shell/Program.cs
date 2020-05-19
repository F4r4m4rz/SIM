using SIM.Core.Commands;
using SIM.Core.Objects;
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
            SIM.Aibel.JSB.ControlObject x = new Aibel.JSB.ControlObject();
            x.PlannedReleaseDatee = new PropertyRelation()
            {
                Origin = x,
                Target = new DateTimePropertyNode()
            };
            x.PlannedReleaseDatee.Target.SetValue(DateTime.Now);

        }
    }
}
