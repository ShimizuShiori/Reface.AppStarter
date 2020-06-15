# 0.6.0

## 2019-05-15

* 对代码进行了结构上的重构
* 将 BaseModuleStarter 和 BaseApplicationStarter 的名称去掉了 Base 前缀
* 调整了事件部分的代码，使得用户在直接使用 ApplicationStarter 和 ModuleStarter 时，不会访问到触发事件的方法
* 移除了 ApplicationStarter 上可以直接监听应用程序启动事件功能，将其一并归入到 Event 属性中
* 应用程序环境中可以访问所有加载的 IModule 信息
* Component 可以自动对泛型进行注册，进一步减少了用户需要自己配置的代码量
* 开始写 RelaseNotes

# 0.6.1

## 2019-05-01

* 修改了 ReleaseNotes 文件名

# 0.6.2

## 2019-05-21

* 为模块添加了新的事件功能，每当一个组件被发现且注入前通知外部

# 0.6.3

## 2019-05-21

* 将应用程序启动事件放置在 autofac 作用域内，并将作用域传入事件参数

# 0.7.0

## 2019-05-22

* 重写底层的实现，重写启动的模块和应用的API
* 封装了简易的对元件注册的方法以致不再直接使用 autofac
* 封闭了简易的对元件获取的方法以致不再直接使用 autofac
* 扩展了一些模块可干预的事件节点

# 0.8.0

## 2019-05-26

* 调整了 ComponentAttribute 的功能，取消了默认组件的功能
* 移除了 Setup 中的 Properties 属性，交由 Context 来完成
* 移除了 Environment 中的 Properties 属性，改为了 Context
* 重新优化了代码结构

# 0.8.1

## 2019-05-26

* 修复了注册时会判断是否已注册的 BUG

# 0.9.0

* rewrite all things

# 0.10.10

* 添加了新的事件，用于监听向 Autofac 容器申请组件时，组件却没有被注册的情景。
* 上述事件允许向 autofac 容器追加元件的注册。
* 向 autofac 注册元件时，只能注册非抽象类
* 以 Func 的方式向 AutofacContainerBuilder 注册元件

# 0.13.0

* 修改了 IAppModule 的 OnUsing 接口，当一个 模块A 需要使用 模块B 时，会将 模块A 的实例提供给 模块B 的 OnUsing 方法中，从而尽可能的不在 AppModule 中使用有参数的构造函数
* 修改了 AppModule 的实现，让其继承于 Attribute ，并使用 AppModule 上的 Attribute 来得到依赖项，简化了定义依赖的方法

# 1.0.0

* 添加了完整的注释

# 1.1.0

## 2020-03-25

* 新增 *Listener* 特征，用于 **事件监听器** 的特征
* 集成了 *CommandBus* 功能
* 新增了 *ComponentCreator** 功能
* 将 *EventBus* 和 *CommandBus* 的注册通过 *ComponentCreator* 完成

# 1.2.0

## 2020-03-26

* 重写了 *AutofacContainerBuilder* 对组件注册的逻辑，不是直接注册到 *autofac* 的 *builder*，而是先记录，当 *AutofacContainerBuilder.Build()* 时才注册
* *AutofacContainerBuilder* 提供按 *ServiceType* 删除注册的功能，允许子模块重新注册 *ServiceType* 的实现类
* 为 *AppSetup* 添加事件，当所有的 *AppModule* 都被加载后，触发 *AllModulesLoaded* 事件
* 添加 *ReplaceCreator* 特征，用于在 *AppMOdule* 替换已有的组件

# 1.5.0

## 2020-03-31

* 重写了 *ReplaceServiceContainerBuilder* 的逻辑，通过监听 *AutofacContainerBuilder.Building* 事件进行组件的替换
* 重写了 *ConfigAppContainerBuilder* 的逻辑，通过监听 *AutofacContainerBuilder.Building* 事件进行组件的替换

# 1.6.0

## 2020-04-07

* **新功能** 系统启动时，生成 *Json Schema* 文件，配置文件可以通过引用此 *schema* 实现提示功能。
* 创建新接口 *IEmptyAppContainer* 表示一个空的容器，让 *App* 忽略对其的托管
* 修改 *AppSetup* 中的一些逻辑，让其现在支持在同一个 *Library* 中存在多个 *AppModule*

# 1.7.0

## 2020-04-10

* 新增 *Predicate* 功能，有利于写出更易阅读的条件判断
* 新增 *AutofacContainerBuilder* 的功能，允许以注册的类型移除已注册的类型

# 1.7.1
## 2020-04-12
* 生成 *JsonSchema* 文件能够对应 *Enum* 类型，并且能够生成相应的描述信息

# 2.0.3
## 2020-04-21
* 新增 *INamespaceFilter* 接口
* *ComponentScanAppModule*、*AutoConfigAppModule* 实现 *INamespaceFilter* 接口
* 新增 *IAppSetupPlugin* 用于对 *AppSetup* 中的一些节点进入介入
* 创建 *NamespaceFilterPlugin* , *AppModuleMethodPlugin* 分别用于实现 *ComponentScanAppModule* 和 *AutoConfigAppModule* 的功能
* 新增 *AppModuleMethodAttribute* 用于为所有标记在 *AppModule* 内的方法的特征提供基本的功能
* *ComponentCreator* , *ConfigCreator* , *ReplaceCreator* 继承于 *AppModuleMethdoAttribute*
* 新增 *ComponentCreatorHandler , ConfigCreatorHandler , ComponentReplaceHandler* 用于实现 *ComponentCreator* , *ConfigCreator* , *ReplaceCreator* 的功能
* 重新定义了 *OnUsing* 的参数为 *AppModuleUsingArguments*
* AppModule 中不允许使用 appSetup.GetScanResult，而是直接将结果放在 *AppModuleUsingArguments* 中

# 2.1.6
## 2020-06-04
* 新增 *AppModulePrepairAttribute* 特征，允许开发者自定义模块准备阶段的行为
* 为 *AppSetup* 添加新的逻辑
    * 在扫描类型前，先扫描出所有的 *IAppModule* 类型
    * 对每一个 *IAppModule* 类型，执行其上的 *AppModulePrepairAttribute.Prepair* 方法
    * 相同类型的 *IAppModule* 只执行一次该方法
* 新增抽象类 *CustomAddPluginsAttribute* ，继承 *AppModulePrepairAttribute* ，允许用户在继承时指定 *IAppSetupPlugin*
* 新增类 *AddPluginsAttribute* 和 *AddDefaultPluginsAttribute* 分别用于让开发者以 Type 指定插件 和 指定默认的插件
* 将原来的组件事件参数命令空间从 AutofacExt 移到 Reface.AppStarter 下
* 新增事件 *ComponentCreated* 
* 添加组件生命周期监听功能，开发者可以在实现类中监听自身的生命周期，目前只设计了以下生命周期节点
    * IOnCreating
    * IOnCreated
* 新增异常 *AppContainerExistsException* ，不允许在 *App* 中存在两个或以上的相同 *IAppContainer* 实例
* 新增 *IWork* 组件，用于表示一个工作单元
    * 可以从 *App* 实例上开启工作单元
    * 每个工作单元内的组件都是同一实例
    * 每个工作单元拥有各个的上下文
    * 每个工作单元可以创建组件和属性注入
    * 每个工作单元可以开启新的工作单元
    * 可以直接利用工作单元发布事件 ( 扩展方法 )
* 允许在 *AppSetup* 阶段预设 *App* 中的 Context 键值对

# 0.0.0
## 000
* 添加了一些扩展方法
* 添加了 PredicateTemplate