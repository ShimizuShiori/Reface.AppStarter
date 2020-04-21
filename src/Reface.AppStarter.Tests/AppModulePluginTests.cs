﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.AppSetupPlugins;
using Reface.AppStarter.AppSetupPlugins.Arguments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Reface.AppStarter.Tests
{
    [TestClass]
    public class AppModulePluginTests
    {
        private const string CONTEXT_KEY_BUILDER_TYPES = "BT";
        private const string CONTEXT_KEY_MODULE_TYPES = "MT";

        #region 辅助组件

        private class TestPlugs : IAppSetupPlugin
        {
            public void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments)
            {
                List<Type> builderTypeList = setup.Context.GetOrCreate(CONTEXT_KEY_BUILDER_TYPES, key => new List<Type>());
                builderTypeList.Add(arguments.AppContainerBuilder.GetType());
            }

            public void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments)
            {
            }

            public void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments)
            {

            }

            public void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments)
            {
                List<Type> list = setup.Context.GetOrCreate(CONTEXT_KEY_MODULE_TYPES, key => new List<Type>());
                list.Add(arguments.AppModule.GetType());
            }
        }

        private class TestBuilder : IAppContainerBuilder
        {
            public IAppContainer Build(AppSetup setup)
            {
                return EmptyAppContainer.Default;
            }

            public void Prepare(AppSetup setup)
            {
            }
        }

        private class Test2Plugs : IAppSetupPlugin
        {
            public void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments)
            {
                List<Type> builderTypeList = setup.Context.GetOrCreate(CONTEXT_KEY_BUILDER_TYPES, key => new List<Type>());
                builderTypeList.Add(arguments.AppContainerBuilder.GetType());
            }

            public void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments)
            {
            }

            public void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments)
            {

            }

            public void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments)
            {
                List<Type> list = setup.Context.GetOrCreate(CONTEXT_KEY_MODULE_TYPES, key => new List<Type>());
                list.Add(arguments.AppModule.GetType());
            }
        }

        private class ThisAppModule : AppModule
        {
            public override void OnUsing(AppModuleUsingArguments args)
            {
                args.AppSetup.GetAppContainerBuilder<TestBuilder>();
            }
        }

        #endregion

        [TestMethod]
        public void AddPlugin()
        {
            var setup = new AppSetup();
            setup.AddPlugin(new TestPlugs());
            Assert.AreEqual(3, setup.Plugins.Count());
        }

        [TestMethod]
        public void AddSamePlugins()
        {
            var setup = new AppSetup();
            setup.AddPlugin(new TestPlugs());
            setup.AddPlugin(new TestPlugs());
            Assert.AreEqual(3, setup.Plugins.Count());
        }
        [TestMethod]
        public void AddDifferentPlugins()
        {
            var setup = new AppSetup();
            setup.AddPlugin(new TestPlugs());
            setup.AddPlugin(new Test2Plugs());
            Assert.AreEqual(4, setup.Plugins.Count());
        }

        [TestMethod]
        public void InvokePluginsWhenStart()
        {
            var setup = new AppSetup();
            setup.AddPlugin(new TestPlugs());
            var app = setup.Start(new ThisAppModule());

            List<Type> builderTypeList = (List<Type>)setup.Context[CONTEXT_KEY_BUILDER_TYPES];
            Assert.AreEqual(4, builderTypeList.Count);

            List<Type> moduleTypeList = (List<Type>)setup.Context[CONTEXT_KEY_MODULE_TYPES];
            Assert.AreEqual(4, moduleTypeList.Count);
        }
    }
}