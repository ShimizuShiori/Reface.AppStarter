using Autofac;
using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModulePrepairs;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.AppSetupPlugins;
using Reface.AppStarter.AppSetupPlugins.Arguments;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacExt;
using Reface.AppStarter.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序安装器。
    /// 这是 Reface.AppStarter 的核心类型，由它配置应用程序，并最终生成 <see cref="Reface.AppStarter.App"/> 类型
    /// </summary>
    public class AppSetup
    {
        #region Public Static Methods

        public static App Start<TAppModule>(AppSetupOptions options)
            where TAppModule : IAppModule, new()
        {
            return new AppSetup(options).Start(new TAppModule());
        }
        public static App Start<TAppModule>()
            where TAppModule : IAppModule, new()
        {
            return AppSetup.Start<TAppModule>(new AppSetupOptions());
        }

        #endregion

        #region Private Fields

        private readonly IList<IAppContainerBuilder> appContainerBuilders
            = new List<IAppContainerBuilder>();
        private readonly Dictionary<Type, IAppSetupPlugin> plugins = new Dictionary<Type, IAppSetupPlugin>();

        private readonly IAllAppModuleTypeCollector allAppModuleTypeCollector;

        private IAppModuleScanner scanner = null;

        #endregion

        #region Public Properties


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
        /// 可以存储自定义数据的上下文，该属性不会被 <see cref="App"/> 继承。
        /// 如果你希望向 <see cref="App"/> 的上下文中添加一些内容，请使用 <see cref="AppContext"/> 属性
        /// </summary>
        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 与 <see cref="Context"/> 属性不同，该上下文对象会被 <see cref="App"/> 继承。
        /// </summary>
        public Dictionary<string, object> AppContext { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFilePath { get; private set; }

        /// <summary>
        /// 工具管理器。<br />
        /// 开发者通过在类型上定义 <see cref="ToolAttribute"/> 特征，<see cref="AppSetup"/> 在启动的过程中，会将标有这些特征的类型注册到一个临时的 IOC 容器中。<br />
        /// 开发者可以使用 <see cref="Tools"/> 以接口的形式创建这些类型的具体实现。<br />
        /// 这些组件不会被带到 <see cref="App"/> 中，仅仅作为一个启动时的工具使用。<br />
        /// 注意：所有标记了 <see cref="ToolAttribute"/> 的类型都会以单例的模式注入到容器中。
        /// </summary>
        public IComponentManager Tools { get; private set; }

        #endregion

        #region Public Events

        /// <summary>
        /// 所有 AppModule 加载完成后的事件
        /// </summary>
        public event EventHandler AllModulesLoaded;

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public AppSetup(AppSetupOptions options)
        {
            this.ConfigFilePath = options.ConfigFilePath;
            this.allAppModuleTypeCollector = options.AllAppModuleTypeCollector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFilePath">配置文件路径，默认是 ./app.json </param>
        public AppSetup(string configFilePath)
            : this(new AppSetupOptions() { ConfigFilePath = configFilePath })
        {
        }

        public AppSetup() : this(new AppSetupOptions())
        {

        }

        #endregion

        #region Publis Methods


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

                PluginInvoker<OnAppContainerBuilderCreatedArguments>
                    .SetArgument(new OnAppContainerBuilderCreatedArguments(builder))
                    .SetPlugins(this.Plugins)
                    .Invoke((t, args) => t.OnAppContainerBuilderCreated(this, args));

                this.appContainerBuilders.Add(builder);
            }
            return (T)builder;
        }

        /// <summary>
        /// 启动应用程序，并返回 <see cref="App"/> 的一个实例。<br />
        /// 执行该方法的流程：<br />
        /// 10. <see cref="AppSetup"/> 将 <see cref="CoreAppModule"/> 和 参数提供的 <see cref="IAppModule"/> 作为根模块扫描所有参与启动的 <see cref="IAppModule"/> 类型 <br />
        /// 11. 根据收集到的类型所在的程序集，注册所有标记了 <see cref="ToolAttribute"/> 的类型到 <see cref="Tools"/> 属性中 <br />
        /// 15. 调用所有 <see cref="IAppModule"/> 上的 <see cref="AppModulePrepairAttribute.Prepair(AppModulePrepareArguments)"/> 方法。<br />
        /// 20. 触发 <see cref="IAppSetupPlugin.OnAllAppModuleTypeCollected(AppSetup, OnAllAppModuleTypeCollectedArguments)"/> 事件 <br />
        /// 30. 调用 <see cref="Use(IAppModule, IAppModule)"/>，参数是 null 和 <see cref="CoreAppModule"/> 用于加载 AppStarter 库中的各种组件 <br />
        /// 40. 调用 <see cref="Use(IAppModule, IAppModule)"/>，参数是 null 和 appModule 用于加载指定的 <see cref="IAppModule"/> 中的各种组件 <br />
        /// 50. 触发 <see cref="AllModulesLoaded"/> 事件 <br />
        /// 60. 遍历所有 <see cref="IAppContainerBuilder"/> 分别调用 <see cref="IAppContainerBuilder.Prepare(AppSetup)"/> 和 <see cref="IAppContainerBuilder.Build(AppSetup)"/> 方法 <br />
        /// 70. 生成 <see cref="App"/> 实例 <br />
        /// 80. 调用所有 <see cref="IAppContainer.OnAppStarted(App)"/> 方法
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        public App Start(IAppModule appModule)
        {
            IEnumerable<IAppModule> rootModules = new IAppModule[]
            {
                new CoreAppModule(),
                appModule
            };

            this.CollectAllAppModuleType(rootModules);
            this.Tools.InjectFields(this);

            rootModules.ForEach(module =>
            {
                this.Use(null, module);
            });

            this.AllModulesLoaded?.Invoke(this, EventArgs.Empty);
            IEnumerable<IAppContainer> appContainers
                = this.appContainerBuilders
                   .ForEach(x => x.Prepare(this))
                   .Select(x => x.Build(this))
                   .ToList();
            App app = new App(appContainers, this.AppContext);
            appContainers.ForEach(x => x.OnAppStarted(app));
            using (var work = app.BeginWork("OnAppStart"))
            {
                work.PublishEvent(new AppStartingEvent(this, app));
                work.PublishEvent(new AppStartedEvent(this, app));
            }
            return app;
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// 扫描指定的模块，返回扫描得到的结果集
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        private IEnumerable<AttributeAndTypeInfo> ScanAppModule(IAppModule appModule)
        {
            IEnumerable<AttributeAndTypeInfo> result = this.scanner.Scan(appModule);

            PluginInvoker<OnAppModuleScannedArguments>
                .SetArgument(new OnAppModuleScannedArguments(appModule, result))
                .SetPlugins(this.Plugins)
                .Invoke((p, args) => p.OnAppModuleScanned(this, args));

            return result;
        }


        /// <summary>
        /// 应用一个模块，当 A 使用 B 时， A 就是 target ，B 就是 appModule
        /// </summary>
        /// <param name="target"></param>
        /// <param name="appModule"></param>
        private void Use(IAppModule target, IAppModule appModule)
        {
            IEnumerable<AttributeAndTypeInfo> targetInfos = this.scanner.Scan(target);

            var arguments = PluginInvoker<AppModuleUsingArguments>
                .SetArgument(new AppModuleUsingArguments(this, appModule, target, targetInfos))
                .SetPlugins(this.Plugins)
                .Invoke((p, args) => p.OnAppModuleBeforeUsing(this, args));

            appModule.OnUsing(arguments);

            PluginInvoker<OnAppModuleUsedArguments>
                .SetArgument(new OnAppModuleUsedArguments(appModule))
                .SetPlugins(this.Plugins)
                .Invoke((p, args) => p.OnAppModuleUsed(this, args));

            IEnumerable<IAppModule> dependentModules = appModule.DependentModules;

            if (dependentModules == null || !dependentModules.Any()) return;

            foreach (IAppModule subAppModule in dependentModules)
            {
                this.Use(appModule, subAppModule);
            }
        }

        /// <summary>
        /// 收集所有模块的类型并触发所有的插件
        /// </summary>
        /// <param name="rootModules"></param>
        private void CollectAllAppModuleType(IEnumerable<IAppModule> rootModules)
        {
            IEnumerable<Type> allAppModuleTypes = this.allAppModuleTypeCollector.Collect(rootModules);

            CreateTools(allAppModuleTypes);

            AppModulePrepareArguments appModulePrepareArguments = new AppModulePrepareArguments(this);

            allAppModuleTypes.ForEach(type => InvokePrepairWhenTypeHasAppModulePrepairAttribute(type, appModulePrepareArguments));

            PluginInvoker<OnAllAppModuleTypeCollectedArguments>
                .SetArgument(new OnAllAppModuleTypeCollectedArguments(allAppModuleTypes))
                .SetPlugins(this.Plugins)
                .Invoke((p, args) => p.OnAllAppModuleTypeCollected(this, args));
        }

        /// <summary>
        /// 初始化工具管理器
        /// </summary>
        /// <param name="allAppModuleTypes"></param>
        private void CreateTools(IEnumerable<Type> allAppModuleTypes)
        {
            ContainerBuilder builder = new ContainerBuilder();
            foreach (var module in allAppModuleTypes)
            {
                foreach (var type in module.Assembly.GetExportedTypes())
                {
                    if (!type.IsTool()) continue;
                    builder.RegisterType(type)
                        .AsImplementedInterfaces()
                        .SingleInstance();
                }
            }
            this.Tools = new ContainerComponentManager(builder.Build());

            PluginInvoker<OnToolsCreatedArguments>
                .SetArgument(new OnToolsCreatedArguments())
                .SetPlugins(this.Plugins)
                .Invoke((p, e) => p.OnToolsCreated(this, e));
        }

        /// <summary>
        /// 当模块类型上标有 <see cref="AppModulePrepareArguments"/> 时，调用 <see cref="AppModulePrepairAttribute.Prepair(AppModulePrepareArguments)"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="arguments"></param>
        private void InvokePrepairWhenTypeHasAppModulePrepairAttribute(Type type, AppModulePrepareArguments arguments)
        {
            IEnumerable<AppModulePrepairAttribute> attrs = type.GetCustomAttributes<AppModulePrepairAttribute>();
            if (!attrs.Any())
                return;

            attrs.ForEach(attr => attr.Prepair(arguments));
        }

        #endregion

    }
}
