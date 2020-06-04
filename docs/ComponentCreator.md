# 在 IAppModule 中通过 Method 注册组件

**版本要求**

* Reface.AppStarter &gt;= 1.1.0


一个单独的模块可能会引用其它的功能库，这些功能库存在它自己的依赖关系，并且可能没有 IOC / DI 的关系。

上述情况，我们无法通过对某个类型加一个 *ComponentAttribute* 来完成对这些组件的注册。
更无法自动注入到其它组件的构造函数中。

为了解决上述问题，我设计了这个方法。
它允许你在 IAppModule 中自定义那些无法参与扫描的类型。

示例 : 

```csharp
[ComponentScanAppModule]
public class MyAppModule : AppModule
{
    [ComponentCreator]
    public ISomeInterface GetSomeInterface(IServiceA serviceA, IServiceB serviceB)
    {
        return new DefaultSomeInterfaceImpl(serviceA, serviceB);
    }
}
```

这是一个普通的 *AppModule*，
它依赖了 *ComponentScanAppModule*
同时，它存在一个标记有 *ComponentCreator* 的方法。
这个方法所产生的实例，会以 *ISomeInterface* 作为 **ServiceType** 注册到 IOC / DI 容器中。

方法中的 *IServiceA* 和 *IServiceB* 来源于 Ioc / DI 容器。

所以你应该准备好类似下面的文件
```csharp
interface IServiceA
{
    
}

[Component]
class ServiceA : IServiceA
{

}

interface IServiceB
{

}

[Component]
class ServiceB : IServiceB
{

}
```

或者你利用 *ComponentCreator* 注册到 *AppModule* 中，例如
```csharp
[ComponentScanAppModule]
public class MyAppModule : AppModule
{
    [ComponentCreator]
    public IServiceA GetServiceA()
    {
        return new ServiceA();
    }

    [ComponentCreator]
    public IServiceB GetServiceB()
    {
        return new ServiceB();
    }

    [ComponentCreator]
    public ISomeInterface GetSomeInterface(IServiceA serviceA, IServiceB serviceB)
    {
        return new DefaultSomeInterfaceImpl(serviceA, serviceB);
    }
}
```

---

[返回](../readme.md#s3-3-3)