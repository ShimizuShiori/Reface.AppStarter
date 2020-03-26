# 覆盖原有的实现类

大部分情况下 *AppModule* 会自己将自己的接口通过 *ComponentAttribute* 进行自动装配。
但是，存在外部需要重新修改这些实现类的情景。

我们可以在定义 *AppModule* 的时候，重新定义指定接口的实现类。

该功能在 1.2.0-beta.1 中第一次加入

```csharp
public class MyAppModule : AppModule
{
    [ReplaceCreator]
    public IMailSender GetMailSender()
    {
        return new MyNewMailSender();
    }
}
```

通过在 *AppModule* 定义标记了 *ReplaceCreatorAttribute* 的方法，可以重定义指定的接口实现类。

**注意事项**
1. **对于同一个接口，*ReplaceCreator* 只能使用一次**

*ReplaceCreator* 的功能实现上是由删除已有注册，重新注册实现的。
当有多个 *AppModule* 都对同一个接口声明了 *ReplaceCreatorAttribute* 时，在 *AppSetup.Start* 时，无法确定各 *AppModule* 加载的顺序，所以最终可能得到非预期的实现类。
因此，系统被设计为一个接口只可以被 *ReplaceCreator* 一次，

2. **返回的类型，作为 *ServiceType* 重新注册**

替换是以 *ServiceType* 为依据进行替换的，
我们会将 *ReplaceCreator* 的返回类型作为 *ServiceType* 删除系统内已有的注册信息，再重新注册。
所以请不要用实现类的类进作为 *method* 的返回类型。