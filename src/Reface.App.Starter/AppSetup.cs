using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    public class AppSetup
    {
        private readonly IList<IAppContainerBuilder> appContainerBuilders
            = new List<IAppContainerBuilder>();
        //private readonly Dictionary<Type, IAppContainerBuilder> existsAppContainerBuilders
        //    = new Dictionary<Type, IAppContainerBuilder>();
        private readonly Dictionary<IAppModule, AppModuleScanResult> appModuleToScannableAttributeAndTypeInfoMap
            = new Dictionary<IAppModule, AppModuleScanResult>();

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
            Type[] types = appModule.GetType().Assembly.GetExportedTypes();
            List<AttributeAndTypeInfo> scannableAttributeAndTypeInfos
                = new List<AttributeAndTypeInfo>();
            foreach (Type type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(ScannableAttribute), true);
                if (objects.Length == 0) continue;
                scannableAttributeAndTypeInfos.Add(
                    new AttributeAndTypeInfo(objects[0] as ScannableAttribute, type)
                );
            }
            this.appModuleToScannableAttributeAndTypeInfoMap[appModule] =
                new AppModuleScanResult(appModule, scannableAttributeAndTypeInfos);
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
                   .Select(x => x.Build(this))
                   .ToList();
            return new App(appContainers);
        }
    }
}
