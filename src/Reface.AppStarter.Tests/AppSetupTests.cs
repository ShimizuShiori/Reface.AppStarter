using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Tests.Configs;
using Reface.AppStarter.Tests.Services;
using System;

namespace Reface.AppStarter.Tests
{
    [TestClass()]
    public class AppSetupTests
    {

        [TestMethod]
        public void TestStart()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            Assert.IsNotNull(componentContainer);
            Assert.IsNotNull(componentContainer.CreateComponent<Class2>());
            Class2 cls2 = componentContainer.CreateComponent<Class2>();
            componentContainer.InjectProperties(cls2);
            int i = app.Context.GetOrCreate<int>("Index", key => 0);
            Assert.AreEqual(2, i);
            Assert.IsNotNull(cls2.Class1);
        }

        [TestMethod]
        public void TestConfigWithSectionExists()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("test"))
            {
                TestConfig config1 = scope.CreateComponent<TestConfig>();
                TestConfig config2 = scope.CreateComponent<TestConfig>();
                Assert.AreEqual("Dev", config1.Mode);
                Assert.AreEqual(config1, config2);
            }
        }

        [TestMethod]
        public void TestConfigWithFileNotExists()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup("./abc.json");
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("test"))
            {
                TestConfig config1 = scope.CreateComponent<TestConfig>();
                TestConfig config2 = scope.CreateComponent<TestConfig>();
                Assert.AreEqual("Mode", config1.Mode);
                Assert.AreEqual(config1, config2);
            }
        }


        [TestMethod]
        public void GetServiceThatRegistedByTestAppModule()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("test"))
            {
                ServiceRegistedByAppModule service
                    = scope.CreateComponent<ServiceRegistedByAppModule>();
                Assert.IsNotNull(service);
                Assert.IsInstanceOfType(service, typeof(DefaultServiceRegistedByAppModule));
            }
        }

        [TestMethod]
        public void TwoSameContainer()
        {
            try
            {
                var app = new AppSetup().Start(new AppModuleThatWillBuildTwoTestAppContainer());
                Assert.Fail("show throw error");
            }
            catch (System.Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(AppContainerExistsException));
            }
        }
    }
}