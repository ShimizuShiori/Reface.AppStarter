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
        private readonly HashSet<string> scannedAssemblyName = new HashSet<string>();

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
            IEnumerable<IAppModule> dependentModules = appModule.DependentModules;
            if (dependentModules == null || !dependentModules.Any()) return;
            foreach (IAppModule subAppModule in dependentModules)
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
            App app = new App(appContainers);
            appContainers.ForEach(x => x.OnAppStarted(app));
            return app;
        }

        private IEnumerable<AttributeAndTypeInfo> GetAttributeAndTypeInfosFromAppModule(IAppModule appModule)
        {
            var assembly = appModule.GetType().Assembly;
            var assemblyName = assembly.FullName;
            if (scannedAssemblyName.Contains(assemblyName))
                return new AttributeAndTypeInfo[] { };
            Type[] types = assembly.GetExportedTypes();
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
            scannedAssemblyName.Add(assemblyName);
            return scannableAttributeAndTypeInfos;
        }
    }
}
