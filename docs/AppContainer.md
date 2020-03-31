# 如何自定义 AppContainer

## 1 什么是 AppContainer

type 在系统中是可以被分类的，比如
* 实例类
* 数据层、持久层、数据仓库
* 服务
* 事件监听
* 命令总线

某些类别的 type 会产系统中产生较为固定的特别的工作方式。
因此，我们需要分类的收集、管理这些 type 。

基于 Reface.AppStarter 的框架下，
我们将对 type 进行分类和管理的地方就是 AppContainer。

Reface.AppStarter 允许开发者自定义包含自身逻辑的 AppContainer，
但是 AppContainer 必须通过 AppContainerBuilder 来构建，
并且当 AppSetup.Start 后通过 AppContainerBuilder.Build 方法来构建出所有的 AppContainer。

## 2 开发流程

### 2.1 创建 AppContainer

AppContainer 必须实现接口 IAppContainer。
IAppContainer 包含以下成员 :
```csharp
public interface IAppContainer : IDisposable
{
    /**
     * 当系统处于准备阶段的事件点
     */
    void OnAppPrepair(App app);

    /**
     * 当系统启动后的事件点
     */
    void OnAppStarted(App app);
}
```
这两个事件点的执行顺序时 :
1. 先执行 AppSetup 中所有 AppContainerBuilder 的 Build() 方法得到所有的 AppContainer 实例。
2. 执行由上面步骤得到的所有 AppContainer.OnAppPrepair(app) 方法
3. 再执行所有 AppContainer.OnAppStarted(app) 方法

当两个 AppContainer 存在一定的关联时，建议不要直接对其操作，而是通过事件来操作。
建议在 OnAppPrepair 阶段对相关的 AppContainer 中的事件进行监听，
相同的，你也可以为自己开发的 AppContainer 预留事件接口，并在需要的时候触发它他。

**注意**
请不要在 OnAppPrepair 阶段触发事件，因为此时不是所有的 AppContainer 都预备好，有些 Listener 可能还没有挂载。

### 2.2 创建 AppContainerBuilder

AppContainer 是通过 AppContainerBuilder 创建得到的。
因此我们除了创建 AppContainer ，更要创建相应的 AppContainerBuilder。

AppContainerBuilder 必须实现 IAppContainerBuilder 接口，

IAppContainerBuilder 接口的定义如下
```csharp
/**
 * 准备构建 AppContainer 阶段
 */
void Prepare(AppSetup setup);
/**
 * 构建 AppContainer
 */
IAppContainer Build(AppSetup setup);
```

与 AppContainer 类似，同样设计了 Prepair 阶段。
设计的意图也是类似的，当多个 AppContainerBuilder 可能存在互操作时，建议使用事件进行操作。并在 Prepair 阶段进行事件的监听。

**例**
AutofacContainerBuilder 是构建 autofac 容器的 Builder，
它也是系统中最重要的一个 AppContainerBuilder。
它会在自身 Build 之前发出 Building 事件。

ConfigAppContainerBuilder、ReplaceServiceContainerBuilder 等其它 AppContainerBuilder 会监听此事件，并在 AutofacContainerBuilder 触发 Building 事件时，向 AutofacContainerBuilder 追加额外的组件。

## 2.3 使用 AppContainerBuilder

在 AppModule 的 OnUsing 方法中使用 AppContainerBuilder 。
关于开发 AppModule 的内容可以参见 [此处](AppModule.md)

---

[返回](../readme.md)