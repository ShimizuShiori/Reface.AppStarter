using ClassLibrary2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class RepeatRegisterTests
    {
        [TestMethod]
        public void Test()
        {
            var app = AppSetup.Start<TestAppModule>();
            using (var work = app.BeginWork(""))
            {
                var s2s = work.CreateComponent<IEnumerable<ISvr2>>();
                Assert.AreEqual(1, s2s.Count());
            }
        }
    }
}
