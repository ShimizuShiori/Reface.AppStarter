using Autofac;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序环境配置类
    /// </summary>
    public class ApplicationEnvironmentSetup
    {
        private readonly TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource;

        private readonly string configFilePath;

        /// <summary>
        /// 容器构建者
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; private set; } = new ContainerBuilder();
        public IComponentRegistion ComponentRegistion { get; private set; }
        public Dictionary<string, object> Context { get; set; } = new Dictionary<string, object>();

        private readonly ApplicationEnvironment applicationEnvironment = new ApplicationEnvironment();
        private readonly JObject jObject;
        private readonly IConfigRegistion configRegistion;

        public ApplicationEnvironmentSetup(string configFilePath = "./application.json")
        {
            this.configFilePath = configFilePath;
            this.triggerComponentCreatingEventAutofacSource
                = new TriggerComponentCreatingEventAutofacSource();
            this.ContainerBuilder.RegisterSource(triggerComponentCreatingEventAutofacSource);
            this.ContainerBuilder.RegisterInstance(applicationEnvironment);
            this.ComponentRegistion = new AutofacComponentRegistion(this.ContainerBuilder);
            if (File.Exists(this.configFilePath))
            {
                string json = File.ReadAllText(this.configFilePath);
                jObject = JObject.Parse(json);
            }
            configRegistion = new DefaultConfigRegistion(this.ContainerBuilder, this.jObject);
        }

        private event EventHandler<ApplicationEnvironmentBuiltEventArgs> ApplicationEnvironmentBuilt;

        /// <summary>
        /// 使用模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public ApplicationEnvironmentSetup Use(IModule module)
        {
            this.triggerComponentCreatingEventAutofacSource.ComponentCreating += (sender, e) =>
            {
                module.OnComponentCreating(e);
            };
            this.ApplicationEnvironmentBuilt += (sender, e) =>
            {
                module.OnApplicationStarted(this.applicationEnvironment, e.ComponentFactory);
            };
            module.OnUsing(this);
            RegisterConfig(module);
            RegisterComponentsInModule(module);
            foreach (IModule subModule in module.GetDependendModules(this))
            {
                this.Use(subModule);
            }
            module.OnUsed(this);
            return this;
        }

        private void RegisterConfig(IModule module)
        {
            this.configRegistion.Register(module);
        }

        /// <summary>
        /// 注册模块中的组件
        /// </summary>
        /// <param name="module"></param>
        private void RegisterComponentsInModule(IModule module)
        {
            foreach (var type in module.GetType().Assembly.GetExportedTypes())
            {
                TypeAndComponentInfo typeAndComponentInfo;
                if (!this.ComponentRegistion.TryRegisterByComponentAttribute(type, out typeAndComponentInfo)) continue;
                module.OnComponentFound(this, typeAndComponentInfo);
            }
        }
        

        private string GetModuleUniqueName(IModule module)
        {
            return module.GetType().FullName;
        }

        internal ApplicationEnvironment Build()
        {
            this.applicationEnvironment.Setup(this);
            using (IComponentFactory componentFactory = this.applicationEnvironment.ComponentFactory.BeginScope("ApplicationEnvironmentBuilt"))
            {
                this.ApplicationEnvironmentBuilt?.Invoke(this, new ApplicationEnvironmentBuiltEventArgs(componentFactory));
            }
            return this.applicationEnvironment;
        }
    }
}
