# ComponentScanAppModule

该 *AppModule* 是 *Reface.AppStarter* 中最常用的一个。

它会将 **依赖它** 的程序集中所有标记了 *ComponentAttribute* 特征的类型都注册到 *IOC / DI* 容器中。

可以非常简便地组装你类型。

假如你有一个 *Library* 的结构如下图所标

```cmd
- MyLib
    - Services
        IUserService.cs
        DefaultUserService.cs
    - Dal
        IUserDao.cs
        DefaultUserDao.cs
    MyLibAppModule.cs
```




---

[Home](../readme.md#ComponentScanAppModule)