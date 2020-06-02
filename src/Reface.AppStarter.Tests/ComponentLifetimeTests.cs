using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Tests.Services;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class ComponentLifetimeTests
    {
        [TestMethod]
        public void TestLifetime()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var worker = app.BeginWork("T"))
            {
                var component = worker.CreateComponent<IComponentListenerToLifetime>();
                Assert.AreEqual(component.GetType(), app.Context["LastCreating"]);
                Assert.AreEqual(component.GetType(), app.Context["LastCreated"]);
            }
        }
    }
}
