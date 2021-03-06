﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Tests.Configs;
using Reface.AppStarter.Tests.Services;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.Tests
{
    [TestClass()]
    public class AppSetupTests
    {
        public class Human { }

        [TestMethod]
        public void TestStart()
        {
            IAppModule appModule = new TestAppModule();
            AppSetup setup = new AppSetup();
            var app = setup.Start(appModule);
            Debug.WriteLine("AppStarted");
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            Assert.IsNotNull(componentContainer);
            Debug.WriteLine("Creating : {0}", typeof(Class2));
            Assert.IsNotNull(componentContainer.CreateComponent<Class2>());
            Debug.WriteLine("Created : {0}", typeof(Class2));
            Debug.WriteLine("Creating : {0}", typeof(Class2));
            Class2 cls2 = componentContainer.CreateComponent<Class2>();
            Debug.WriteLine("Created : {0}", typeof(Class2));
            Debug.WriteLine("Injecting : {0}", typeof(Class2));
            componentContainer.InjectProperties(cls2);
            Debug.WriteLine("Injected : {0}", typeof(Class2));

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

        [TestMethod]
        public void ChangeInstance()
        {
            var app = AppSetup.Start<TestAppModule>();
            IComponentContainer container = app.GetAppContainer<IComponentContainer>();
            container.ComponentCreating += (sender, e) =>
            {
                if (e.CreatedObject is IIdService)
                    e.Replace(new EmptyIdService());
            };
            using (IWork work = app.BeginWork("Test"))
            {
                IIdService service = work.CreateComponent<IIdService>();
                Assert.IsInstanceOfType(service, typeof(EmptyIdService));
            }
        }


    }
}