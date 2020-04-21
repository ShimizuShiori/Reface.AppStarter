using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Tests.ModuleA.Configs;
using Reface.AppStarter.Tests.ModuleA.Dal;
using Reface.AppStarter.Tests.ModuleA.Services;
using Reface.AppStarter.Tests.ModuleB.Configs;
using Reface.AppStarter.Tests.ModuleB.Services;
using Reface.AppStarter.Tests.Services;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class APlusBTests
    {
        [TestMethod]
        public void Start()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
        }

        [TestMethod]
        public void GetServiceA()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            IServiceA serviceA = container.CreateComponent<IServiceA>();
            Assert.IsInstanceOfType(serviceA, typeof(ServiceA));
        }

        [TestMethod]
        public void GetServiceB()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            IServiceB serviceA = container.CreateComponent<IServiceB>();
            Assert.IsInstanceOfType(serviceA, typeof(ServiceB));
        }

        [TestMethod]
        public void IServiceNotExists()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();

            Assert.ThrowsException<ComponentNotRegistedException>(() =>
            {
                IService serviceA = container.CreateComponent<IService>();
            });
        }



        [TestMethod]
        public void GetConfigA()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            var config = container.CreateComponent<ConfigA>();
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void GetConfigB()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            var config = container.CreateComponent<ConfigB>();
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void ADaoNotExists()
        {
            var app = new AppSetup().Start(new APlusBAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            Assert.ThrowsException<ComponentNotRegistedException>(() => 
            {
                container.CreateComponent<IADao>();
            });
        }
    }
}
