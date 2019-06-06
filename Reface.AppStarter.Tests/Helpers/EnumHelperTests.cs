using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        enum Flags
        {
            A = 1,
            B = 2,
            C = 4,
            E = 8
        }

        [TestMethod()]
        public void HasFlagTest()
        {
            Assert.AreEqual(true, EnumHelper.HasFlag(Flags.A | Flags.B, Flags.A));
            Assert.AreEqual(false, EnumHelper.HasFlag(Flags.C | Flags.B, Flags.A));
        }
    }
}