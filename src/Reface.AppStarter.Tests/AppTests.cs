using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Services;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void BeginWork()
        {
            var app = new AppSetup().Start(new TestAppModule());
            IIdService t1, t2, t3;
            using (var work = app.BeginWork("t1"))
            {
                t1 = work.CreateComponent<IIdService>();
                t2 = work.CreateComponent<IIdService>();
                Assert.AreEqual(t1.Id, t2.Id);
            }
            using (var work = app.BeginWork("t2"))
            {
                t3 = work.CreateComponent<IIdService>();
                Assert.AreNotEqual(t1.Id, t3.Id);
            }
        }

        [TestMethod]
        public void WorkInWork()
        {
            var app = new AppSetup().Start(new TestAppModule());
            IIdService t1, t2, t3, t4
                ;
            using (var w = app.BeginWork("t"))
            {
                t1 = w.CreateComponent<IIdService>();
                using (var w1 = w.BeginWork("t1"))
                {
                    t2 = w1.CreateComponent<IIdService>();
                    t3 = w1.CreateComponent<IIdService>();

                    Assert.AreEqual(t2.Id, t3.Id);
                    Assert.AreNotEqual(t1.Id, t2.Id);
                }
                t4 = w.CreateComponent<IIdService>();
                Assert.AreEqual(t1.Id, t4.Id);
            }
        }
    }
}
