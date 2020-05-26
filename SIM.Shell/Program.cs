using SIM.Core.Commands;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new SimNodeFactory();
            Test(factory);
        }

        static void Test(SimNodeFactory factory)
        {
            var x = factory.GetConstructionArguments<SIM.Aibel.JSB.SDI>();
            var y = x.GetExpectedTypes();
            x.AssignArgumentValues(new StringPropertyNode("Hello"), new StringPropertyNode("Bye"), new SIM.Aibel.JSB.Section());
            var sdi = factory.New(x);

            // Making a relation
            var rel = factory.GetConstructionArguments<SIM.Aibel.JSB.HasCO>();

            //var co = factory.New<SIM.Aibel.JSB.CO>();
        }
    }
}
