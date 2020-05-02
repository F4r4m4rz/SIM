using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Tests.Dynamic
{
    [TestClass]
    [DynamicCategory]
    public class DynamicNode_Should
    {
        private static DynamicNode sur;

        [ClassInitialize]
        public static void Initialization(TestContext testContext)
        {
            sur = new DynamicNode();
        }

        [TestMethod]
        public void HaveTypeOfNode()
        {
            Assert.IsTrue(sur.DerivedFrom.Equals(typeof(Node)));
        }
    }
}
