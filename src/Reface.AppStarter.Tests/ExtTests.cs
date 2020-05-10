using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Events;

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
    }
}
