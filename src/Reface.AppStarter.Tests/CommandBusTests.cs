using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.Commands;
using Reface.CommandBus;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class CommandBusTests
    {
        public IComponentContainer ComponentContainer { get; private set; }

        [TestInitialize]
        public void Init()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            ComponentContainer = componentContainer.BeginScope("TEST");
        }

        [TestMethod]
        public void GetCommandBus()
        {
            ICommandBus commandBus = ComponentContainer.CreateComponent<ICommandBus>();
            Assert.IsNotNull(commandBus);
            Assert.IsInstanceOfType(commandBus, typeof(DefaultCommandBus));
        }

        [TestMethod]
        public void DispatchCreateUserCommand()
        {
            ICommandBus commandBus = ComponentContainer.CreateComponent<ICommandBus>();
            CreateUserCommand command = new CreateUserCommand("Shiori", "987654321");
            string newId = commandBus.Dispatch<CreateUserCommand, string>(command);
            Assert.AreEqual("1234", newId);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ComponentContainer.Dispose();
        }
    }
}
