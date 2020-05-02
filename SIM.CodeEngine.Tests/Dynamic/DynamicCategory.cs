using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Tests.Dynamic
{
    public class DynamicCategory : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories => new[] { "Dynamic classes" };
    }
}
