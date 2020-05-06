using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.AppSetupPlugins;
using Reface.AppStarter.AppSetupPlugins.Arguments;
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
        private readonly Dictionary<Type, IAppSetupPlugin> plugins = new Dictionary<Type, IAppSetupPlugin>();

        /// <summary>
        /// 所有的插件
        /// </summary>
        public IEnumerable<IAppSetupPlugin> Plugins
        {
            get
            {
                return plugins.Values;
            }
        }

        /// <summary>
        /// 可以存储自定义数据的上下文
        /// </summary>
        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 所有 AppModule 加载完成后的事件
        /// </summary>
        public event EventHandler AllModulesLoaded;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFilePath { get; private set; }

        /// <summary>
        /// 缓存那些已经扫描过的结果，提高运行时的速度。
        /// </summary>
        private readonly Dictionary<string, IEnumerable<AttributeAndTypeInfo>>
            sacennedAssemlyNameToInfoCache = new Dictionary<string, IEnumerable<AttributeAndTypeInfo>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFilePath">配置文件路径，默认是 ./app.json </param>
        public AppSetup(string configFilePath = "./app.json")
        {
            this.ConfigFilePath = configFilePath;
            this.AddPlugin(new NamespaceFilterPlugin());
            this.AddPlugin(new AppModuleMethodPlugin());
        }

        /// <summary>
        /// 添加一个插件
        /// </summary>
        /// <param name="plugin"></param>
        public void AddPlugin(IAppSetupPlugin plugin)
        {
            Type type = plugin.GetType();
            if (this.plugins.ContainsKey(type)) return;
            this.plugins[type] = plugin;
        }

        /// <summary>
        /// 调用所有的插件
        /// </summary>
        /// <param name="parameterBuilder"></param>
        /// <param name="action"></param>
        public TParameter InvokePlugins<TParameter>(Func<TParameter> parameterBuilder, Action<TParameter, IAppSetupPlugin> action)
        {
            TParameter parameter = parameterBuilder();
            this.Plugins.ForEach(p => action(parameter, p));
            return parameter;
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
                this.InvokePlugins(
                    () => new OnAppContainerBuilderCreatedArguments(builder),
                    (para, plug) => plug.OnAppContainerBuilderCreated(this, para)
                );
                this.appContainerBuilders.Add(builder);
            }
            return (T)builder;
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
            this.AllModulesLoaded?.Invoke(this, EventArgs.Empty);
            IEnumerable<IAppContainer> appContainers
                = this.appContainerBuilders
                   .ForEach(x => x.Prepare(this))
                   .Select(x => x.Build(this))
                   .ToList();
            App app = new App(appContainers);
            appContainers.ForEach(x => x.OnAppStarted(app));
            return app;
        }

        #region Private Methods


        /// <summary>
        /// 扫描指定的模块，返回扫描得到的结果集
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        private IEnumerable<AttributeAndTypeInfo> ScanAppModule(IAppModule appModule)
        {
            var assembly = appModule.GetType().Assembly;
            var assemblyName = assembly.FullName;
            IEnumerable<AttributeAndTypeInfo> result;

            if (sacennedAssemlyNameToInfoCache.TryGetValue(assemblyName, out result))
            {
                return result;
            }

            Type[] types = assembly.GetExportedTypes();
            IList<AttributeAndTypeInfo> scannableAttributeAndTypeInfos
                 = new List<AttributeAndTypeInfo>();
            foreach (Type type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(ScannableAttribute), true);
                if (objects.Length == 0) continue;

                foreach (var obj in objects)
                {
                    AttributeAndTypeInfo attributeAndTypeInfo
                        = new AttributeAndTypeInfo(obj as ScannableAttribute, type);
                    scannableAttributeAndTypeInfos.Add(
                        attributeAndTypeInfo
                    );

                }
            }

            sacennedAssemlyNameToInfoCache[assemblyName] = scannableAttributeAndTypeInfos;
            return scannableAttributeAndTypeInfos;
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

            AppModuleUsingArguments arguments = this.InvokePlugins(
                    () => new AppModuleUsingArguments(this, appModule, target, this.GetScanResult(target).ScannableAttributeAndTypeInfos),
                    (para, plug) => plug.OnAppModuleBeforeUsing(this, para)
                );

            appModule.OnUsing(arguments);
            this.InvokePlugins(
                () => new OnAppModuleUsedArguments(appModule),
                (para, plug) => plug.OnAppModuleUsed(this, para)
                );

            IEnumerable<IAppModule> dependentModules = appModule.DependentModules;
            if (dependentModules == null || !dependentModules.Any()) return;
            foreach (IAppModule subAppModule in dependentModules)
            {
                this.Use(appModule, subAppModule);
            }
        }



        /// <summary>
        /// 获取指定模块的扫描结果
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        private AppModuleScanResult GetScanResult(IAppModule appModule)
        {
            if (appModule != null)
                return this.appModuleToScannableAttributeAndTypeInfoMap[appModule];
            return AppModuleScanResult.Empty;
        }

        #endregion

    }
}
