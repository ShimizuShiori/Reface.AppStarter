using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Tests.Configs;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class ConfigCreatorTests
    {
        [TestMethod]
        public void GetConfig()
        {
            var app = new AppSetup().Start(new TestAppModule());
            var container = app.GetAppContainer<IComponentContainer>();
            var config = container.CreateComponent<SomeConfigWithoutAttribute>();
            Assert.AreEqual("NoName", config.PlayerName);
            Assert.AreEqual(1, config.RootId);
            Assert.AreEqual(true, config.CanGenerateMinusNumber);
            Assert.AreEqual(123456, config.Seed);
        }
    }
}
