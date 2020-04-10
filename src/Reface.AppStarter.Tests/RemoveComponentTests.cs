using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Tests.Configs;
using Reface.AppStarter.Tests.Services;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class RemoveComponentTests
    {
        [TestMethod]
        public void RemoveAsInterfaces()
        {
            var setup = new AppSetup();
            var app = setup.Start(new RemoveComponentByComponentTypeTestAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            using (var scope = container.BeginScope("TEST"))
            {
                Assert.ThrowsException<ComponentNotRegistedException>(() => scope.CreateComponent<IService>());
            }
        }

        [TestMethod]
        public void RemoveAsSelf()
        {
            var app = new AppSetup().Start(new RemoveComponentByComponentTypeTestAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            using (var scope = container.BeginScope("TEST"))
            {
                Assert.ThrowsException<ComponentNotRegistedException>(() =>
                {
                    scope.CreateComponent<Test2Config>();
                });
            }
        }
    }
}
