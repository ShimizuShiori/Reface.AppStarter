﻿using ClassLibrary1.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Errors;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class ReplaceServiceTests
    {
        [TestMethod]
        public void RemoveService()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("TEST"))
            {
                ICL1Service service = scope.CreateComponent<ICL1Service>();
                Assert.AreEqual("TEST", service.GetName());
            }
        }
    }
}
