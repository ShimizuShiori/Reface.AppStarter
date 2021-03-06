﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.Commands;
using Reface.CommandBus;
using System;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class CommandBusTests
    {
        public IComponentContainer ComponentContainer { get; private set; }

        [TestInitialize]
        public void Init()
        {
            Console.WriteLine("CommandBusTests.Init");
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
            commandBus.Dispatch(command);
            Assert.AreEqual("1234", command.CreateResult);
        }

        [TestCleanup]
        public void Cleanup()
        {
            ComponentContainer.Dispose();
        }
    }
}
