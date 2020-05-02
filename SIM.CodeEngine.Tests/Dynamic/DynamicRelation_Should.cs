using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIM.CodeEngine.Dynamic;
using SIM.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Tests.Dynamic
{
    [TestClass]
    [DynamicCategory]
    public class DynamicRelation_Should
    {
        private static DynamicRelation sur;

        [ClassInitialize]
        public static void Initialization(TestContext testContext)
        {
            sur = new DynamicRelation();
        }

        [TestMethod]
        public void HaveTypeOfRelation()
        {
            Assert.IsTrue(sur.DerivedFrom.Equals(typeof(Relation)));
        }
    }
}
