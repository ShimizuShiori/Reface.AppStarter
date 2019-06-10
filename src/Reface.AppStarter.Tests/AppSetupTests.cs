using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.Configs;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass()]
    public class AppSetupTests
    {
        [TestMethod]
        public void TestUse()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            setup.Use(appModule);

            AppModuleScanResult appModuleScanResult = setup.GetScanResult(appModule);

            Assert.AreEqual(appModule, appModuleScanResult.AppModule);
            //Assert.AreEqual(4, appModuleScanResult.ScannableAttributeAndTypeInfos.Count());
        }

        [TestMethod]
        public void TestGetContainerBuilderTwiceAndResultAreSame()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();

            TestContainerBuilder testContainerBuilder1
                = setup.GetAppContainerBuilder<TestContainerBuilder>();
            TestContainerBuilder testContainerBuilder2
                = setup.GetAppContainerBuilder<TestContainerBuilder>();
            Assert.AreEqual(testContainerBuilder1, testContainerBuilder2);
        }

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
        public void TestConfigWithSectionNotExists()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("test"))
            {
                Test2Config config1 = scope.CreateComponent<Test2Config>();
                Test2Config config2 = scope.CreateComponent<Test2Config>();
                Assert.AreEqual("Test2", config1.Mode);
                Assert.AreEqual(config1, config2);
            }
        }
    }
}