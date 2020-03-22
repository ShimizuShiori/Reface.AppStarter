using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序安装器。
    /// 这是 Reface.AppStarter 的核心类型，由它配置应用程序，并最终生成 <see cref="Reface.AppStarter.App"/> 类型
    /// </summary>
    public class AppSetup
    {
        private readonly IList<IAppContainerBuilder> appContainerBuilders
            = new List<IAppContainerBuilder>();
        private readonly Dictionary<IAppModule, AppModuleScanResult> appModuleToScannableAttributeAndTypeInfoMap
            = new Dictionary<IAppModule, AppModuleScanResult>();

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFilePath { get; private set; }
        private readonly HashSet<string> scannedAssemblyName = new HashSet<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFilePath">配置文件路径，默认是 ./app.json </param>
        public AppSetup(string configFilePath = "./app.json")
        {
            this.ConfigFilePath = configFilePath;
        }

        /// <summary>
        /// 获取指定类型的应用程序构建器
        /// </summary>
        /// <typeparam name="T">必须实现 <see cref="IAppContainerBuilder"/> 接口</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// 获取指定模块的扫描结果
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        public AppModuleScanResult GetScanResult(IAppModule appModule)
        {
            return this.appModuleToScannableAttributeAndTypeInfoMap[appModule];
        }

        /// <summary>
        /// 应用一个模块，当 A 使用 B 时， A 就是 target ，B 就是 appModule
        /// </summary>
        /// <param name="target"></param>
        /// <param name="appModule"></param>
        private void Use(IAppModule target, IAppModule appModule)
        {
            IEnumerable<AttributeAndTypeInfo> attributeAndTypeInfos = this.ScanAppModule(appModule);
            this.appModuleToScannableAttributeAndTypeInfoMap[appModule] = new AppModuleScanResult(appModule, attributeAndTypeInfos);
            appModule.OnUsing(this, target);
            IEnumerable<IAppModule> dependentModules = appModule.DependentModules;
            if (dependentModules == null || !dependentModules.Any()) return;
            foreach (IAppModule subAppModule in dependentModules)
            {
                this.Use(appModule, subAppModule);
            }
        }

        /// <summary>
        /// 启动应用程序，并返回 <see cref="App"/> 的一个实例
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        public App Start(IAppModule appModule)
        {
            CoreAppModule coreAppModule = new CoreAppModule();
            this.Use(null, coreAppModule);
            this.Use(null, appModule);
            IEnumerable<IAppContainer> appContainers
                = this.appContainerBuilders
                   .ForEach(x => x.Prepare(this))
                   .Select(x => x.Build(this))
                   .ToList();
            App app = new App(appContainers);
            appContainers.ForEach(x => x.OnAppStarted(app));
            return app;
        }

        /// <summary>
        /// 扫描指定的模块，返回扫描得到的结果集
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        private IEnumerable<AttributeAndTypeInfo> ScanAppModule(IAppModule appModule)
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
