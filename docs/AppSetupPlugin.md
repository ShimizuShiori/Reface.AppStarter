**版本要求**

* Reface.AppStarter >= 2.0.3 

---

*AppSetup* 在启动过程中会经历以下几个阶段

1. 收集所有参与启动的 *IAppModule* 的类型
2. 执行所有 *IAppModule* 类型上标记的 *AppModulePrepairAttribute* 实例的 *Prepair* 方法
3. 从根模块开始加载模块
    3.1 扫描当前模块中的所有类型，扫描的依据是类型上标有 *ScannableAttribute* 特征
    3.2 执行当前模块依赖的模块的 OnUsing 方法
    3.3 加载依赖模块
4. 依次调用 *AppSetup* 内的 *IAppContainerBuilder* 的 *Prepair* 和 *Build* 方法得到所有的 *IAppContainer*
5. 构建 *App* 实例
6. 调用所有 *IAppContainer* 的 *OnAppStarted* 方法


通过插件，开发者可以对其中一些功能进行增强。

编写的 **插件** 必须实现 *IAppSetupPlugin* 接口。

### IAppSetupPlugin 的定义

```csharp
using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.AppSetupPlugins.Arguments;

namespace Reface.AppStarter.AppSetupPlugins
{
    /// <summary>
    /// 在 <see cref="AppSetup"/> 工作周期中的插件
    /// </summary>
    public interface IAppSetupPlugin
    {
        /// <summary>
        /// 当一个 <see cref="IAppContainerBuilder"/> 被创建后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments);

        /// <summary>
        /// 调用一个 <see cref="IAppModule.OnUsing(AppModuleUsingArguments)"/> 前的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments);

        /// <summary>
        /// 当 <see cref="IAppModule"/> 完成了 <see cref="IAppModule.OnUsing(AppModuleUsingArguments)"/> 后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments);

        /// <summary>
        /// 当 <see cref="AppSetup.ScanAppModule(IAppModule)"/> 完成后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments);

        /// <summary>
        /// 当收集到了系统中所有的 <see cref="IAppModule"/> 类型后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAllAppModuleTypeCollected(AppSetup setup, OnAllAppModuleTypeCollectedArguments arguments);
    }
}
```

**OnAppContainerBuilderCreated**

在 *IAppModule* 的 *OnUsing* 方法中，开发者可以通过 *AppSetup.GetAppContainerBuilder* 方法来获取一个容器构建器。

*AppSetup* 会尝试将已有的相同类型的构建器返回给请求者，当不存在时，会通过无构造函数创建一个新的返回，并将其缓存。

当创建出新的构建器时，会调用所有插件的该方法。

开发者可以在此方法中获取被创建出的 **构建器** 实例。

**OnAppModuleBeforeUsing**

在执行 *IAppModule.OnUsing* 方法前的事件。

开发者可以通过该方法重新定义被扫描出的组件信息。

**OnAppModuleUsed**

在执行 *IAppModule.OnUsed* 方法后的事件。

**OnAppModuleScanned**

在扫描了模块的所有成员后的事件。

开发者可以通过该方法获取所有扫描得到的组件信息。

**OnAllAppModuleTypeCollected**

当收集到启动的所有 *IAppModule* 类型后的事件。

开发者可以通过此方法获取所有的模块类型。（去重后的集合）

> 更多方法会在以后的更新中追加。

### 请从 AppSetupPlugin 继承

为了防止未来对插件方法的扩展不影响你代码的编译，请将你的插件从 *AppSetupPlugin* 继承。

该类只是一个实现了 *IAppSetupPlugin* ，并将所有方法设为虚方法的类型。

本身没有任何实现。

### 如何添加插件

1. 通过 *AppSetup.AddPlugin* 添加

```csharp
var setup = new AppSetup();
setup.AddPlugin(new MyPlugin());
```

2. 通过 *AppModulePrepairAttribute* 添加

**需要 Reface.AppStarter 的版本号不低于 2.1.0**

有关 *AppModulePrepairAttribute* 可以阅读 [此文档](AppModulePrepairAttribute.md)。

可以通过已经开发好的 *AddPluginsAttribute* 和 *CustomAddPluginsAttribute* 来添加插件。

---

[返回](../readme.md#s5-3)