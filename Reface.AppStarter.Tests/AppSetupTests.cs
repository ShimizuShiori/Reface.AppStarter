using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(3, appModuleScanResult.ScannableAttributeAndTypeInfos.Count());
        }

        [TestMethod]
        public void TestGetContainerBuilder()
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
        }
    }
}