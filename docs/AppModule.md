# 开发 AppModule

AppModule 是 Reface.AppStarter 框架下的构建基本要素。
它的主要作用是
* 添加依赖的 AppModule
* 增强被依赖的 AppModule 的功能
* 向 AppSetup.Start 的过程中添加逻辑的功能和操作

AppModule 必须实现 IAppModule 接口，
IAppModule 提供如下的成员
```csharp
// 该 AppModule 所依赖的其它 AppModule
IEnumerable<IAppModule> DependentModules { get; }

// 当一个 AppModule 被依赖时，可以添加的额外操作
void OnUsing(AppModuleUsingArguments arguments);
```

**注意**

当 A 引用 B 时，B 应当出现在 A 的 DependentModules 中，而 A 会作用参数出现 B 的 OnUsing 的 targetModule 中。

系统中提供了更为便捷的 *AppModule* ，可以让开发者避免直接从接口进行实现。

通过将你的 AppModule 继承于 *AppModule* ，你可以直接通过 Attribute 定义依赖项。

```csharp
[ComponentnScanAppModule]
[AutoConfigAppModule]
public class YourAppModule : AppModule
{
    // 这可以 重写 OnUsing 方法
    // 还可以 重写 AppendOtherModules 方法手动添加更多的 AppModule
    // 还可以 添加 [ComponentCreator]
    // 还可以 添加 [ReplaceCreator]
    // 还可以 添加 [ConfigCreator]
}
```

**OnUsing** 方法

当我们开发一个业务模块的时候，OnUsing 方法是开发者们用不到的，不用去重写它。

但是当我们编写一个功能增强模块时，就会需要使用到了。

例如，当我们编写一个 AOP 增强模块时，我们会需要对依赖 AOP 的模块进行增强。

我们希望扫描那些模块，得知哪些类型需要进行 AOP 增强，并且去增强它们。

AppSetup 在调用 OnUsing 方法之前，会对 targetModule 中的所有 type 进行扫描。

因此，我们可以在 OnUsing 方法中通过 AppSetup.GetScanResult(targetModule) 方法得到扫描的结果。

扫描的结果由类型名和Attribute组成，你可以根据自己的需求过滤这些类型。
我们不建议直接在 AppModule.OnUsing 方法中对这些类型进行复杂的操作。

而是将这些操作放到 AppContainerBuilder 中。

开发者可以通过 AppSetup.GetAppContainerBuilder<TAppContainerBuilder> 来获取一个 Builder，并通过 Builder 进行增强操作。

**注意**

你不需要手动 new 一个 AppContainerBuilder，你只需要为你的 AppContainerBuilder 设计一个无参数的构造函数。

AppSetup 为了能够保存 Builder 的单例性，因为 AppSetup 会根据情况创建这些 Builder 的实例并返回。

**例如**

AutoConfigAppModule，
它在 OnUsing 阶段，把具有 ConfigAttribute 的 type 交给 ConfigAppContainerBuilder
ConfigAppContainerBuilder 会在 Prepair 阶段监听 AutofacContainerBuilder 的 Building 方法
并在监听的方法中读取配置文件，反序列化，最终将配置类注册到 autofac 容器中。

---
[返回](../readme.md)