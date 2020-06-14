using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Events;
using System.Collections.Generic;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class ExtTests
    {
        [TestMethod]
        public void PublishEvent()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork("t1"))
            {
                work.PublishEvent(new TestEvent(this));
                Assert.AreEqual("TEST", app.Context["TEST"]);
            }
        }

        [TestMethod]
        [DataRow(new string[] { "a", "b", "c" }, ",", "a,b,c")]
        [DataRow(null, ",", "")]
        [DataRow(new string[] { "a" }, ",", "a")]
        [DataRow(new string[] { "a", "b", "c" }, null, "abc")]
        public void JoinTest(string[] list, string joiner, string result)
        {
            Assert.AreEqual(result, list.Join(joiner));
        }
    }
}
