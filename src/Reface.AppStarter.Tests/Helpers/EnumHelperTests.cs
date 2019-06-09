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
            Zero = 0,
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

        [TestMethod]
        public void TestRemoveFlag()
        {
            Assert.AreEqual(Flags.A, EnumHelper.RemoveFlag(Flags.B | Flags.A, Flags.B));
            Assert.AreEqual(Flags.Zero, EnumHelper.RemoveFlag(Flags.B | Flags.A, Flags.B | Flags.A));
            Assert.AreEqual(Flags.A | Flags.B, EnumHelper.RemoveFlag(Flags.B | Flags.A, Flags.C));
        }
    }
}