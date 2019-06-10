using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    public class AppSetup
    {
        private readonly IList<IAppContainerBuilder> appContainerBuilders
            = new List<IAppContainerBuilder>();
        private readonly Dictionary<IAppModule, AppModuleScanResult> appModuleToScannableAttributeAndTypeInfoMap
            = new Dictionary<IAppModule, AppModuleScanResult>();
        public string ConfigFilePath { get; private set; }

        public AppSetup(string configFilePath = "./app.json")
        {
            this.ConfigFilePath = configFilePath;
        }

        public T GetAppContainerBuilder<T>()
            where T : IAppContainerBuilder, new()
        {
            Type type = typeof(T);
            IAppContainerBuilder builder
                =
                this.appContainerBuilders.Where(x => type.IsAssignableFrom(x.GetType()))
                .FirstOrDefault();
            if (builder == null)
            {
                builder = new T();
                this.appContainerBuilders.Add(builder);
            }
            return (T)builder;
        }

        public AppModuleScanResult GetScanResult(IAppModule appModule)
        {
            return this.appModuleToScannableAttributeAndTypeInfoMap[appModule];
        }

        public void Use(IAppModule appModule)
        {
            IEnumerable<AttributeAndTypeInfo> attributeAndTypeInfos = this.GetAttributeAndTypeInfosFromAppModule(appModule);
            this.appModuleToScannableAttributeAndTypeInfoMap[appModule] = new AppModuleScanResult(appModule, attributeAndTypeInfos);
            appModule.OnUsing(this);
            if (appModule.DependentModules == null) return;
            foreach (IAppModule subAppModule in appModule.DependentModules)
            {
                this.Use(subAppModule);
            }
        }

        public App Start(IAppModule appModule)
        {
            CoreAppModule coreAppModule = new CoreAppModule();
            this.Use(coreAppModule);
            this.Use(new ComponentScanAppModule(coreAppModule));
            this.Use(appModule);
            IEnumerable<IAppContainer> appContainers
                = this.appContainerBuilders
                   .ForEach(x => x.Prepare(this))
                   .Select(x => x.Build(this))
                   .ToList();
            return new App(appContainers);
        }

        private IEnumerable<AttributeAndTypeInfo> GetAttributeAndTypeInfosFromAppModule(IAppModule appModule)
        {
            Type[] types = appModule.GetType().Assembly.GetExportedTypes();
            List<AttributeAndTypeInfo> scannableAttributeAndTypeInfos
                 = new List<AttributeAndTypeInfo>();
            foreach (Type type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(ScannableAttribute), true);
                if (objects.Length == 0) continue;
                AttributeAndTypeInfo attributeAndTypeInfo
                    = new AttributeAndTypeInfo(objects[0] as ScannableAttribute, type);
                scannableAttributeAndTypeInfos.Add(
                    attributeAndTypeInfo
                );
            }
            return scannableAttributeAndTypeInfos;
        }
    }
}
