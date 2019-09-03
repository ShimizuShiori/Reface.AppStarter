# Reface.AppStarter

## 1 简介

该框架定义了一种程序的启动方式，它允许模块定义它的依赖模块，并在程序启动时，根据依赖关系从向上向下扫描模块中的组件，并对组件进行分类。扫描分类后的组件信息可以被使用进行不同的初始化等自定义操作。
它还可以指定模块在被使用时进行一些其它额外的操作。

## 2 主要功能

* 定义模块的依赖模块
* 在系统启动时，对启动模块及其依赖的所有模块进行类型的扫描
* 可以对类型进行分类，以便在扫描后区别处理
* 允许创建新的分类
* 允许自定义模块在被使用时进行一些额外的扩展
* 集成了 [事件总线](https://github.com/ShimizuShiori/EventBus) 功能
* 自动装配配置类

## 3 主要对象

### 3.1 AppSetup

该类型负责对应用程序的初始化和安装。
原则上，构建一个新的 **AppSetup** ，并使用该实例的 **Start(IAppModule appModule)** 方法，便可以使用 **Reface.AppStarter** 中的所有功能。
```csharp
var myApp = new MyStartAppModule(); // 你自已的启动模块
var setup = new AppSetup();
var app = setup.Start(myApp); // 这样便启用了所有功能
```

### 3.2 App

当 **AppSetup.Start** 执行成功后，会得到一个 App 实例，App 仅能获取各种组件的容器（后文会有说明）。

### 3.3 IAppModule

该接口用来描述一个应用程序模块。
描述的内容只有两项
* 依赖的其它模块
* 加载时的自定义行为

使用 **Reface.AppStarter** 进行模块化加载就意味着，你需要对你的每一个模块都要创建一个 **IAppModule** 用于定义依赖关系。
你还需要为你的启动项定义一个 **IAppModule** 用于 **AppSetup.Start(module)**

### 3.4 可扫描组件

**AppSetup** 会扫描每一个 **IAppModule** 所在程序集中的每一个类型，并将标有 **Scannable** 特征的类型进行提取和提存，以便进行各种扩展。

### 3.5 IAppContainer / IAppContainerBuilder

应用容器，以及容器构建器。
构建器如其名，是用来构建容器用的。
对于一个 **AppSetup** 实例，每种类型的构建器是单例的，当开发者通过 **IAppModule.Use** 方法进行扩展时，将操作同样构建器，以达到生成容器的准确性。
> 例. AutofacContainerBuilder 构建器会在每个模块加载后，将 autofac 的 ContainerBuilder 进行元件的注册

所有的容器构建器都有 Build 方法，当 AppSetup.Start 把所有模块扫描完成后，会统一执行每个构建器的 Build 方法，并将生成的容器放在 App 内部，以便访问。

程序运行时，可以通过注入 App 来得到 app 的实例，并访问其中的所有容器。

## 4 所有特征

### ScannableAttribute

表示这个类型允许被扫描

### ComponentAttribute

表示这个类型将被注册到 autofac 容器中

### ConfigAttribute

表示这个类型中的属性值会被配置文件自动注入

## 5 基本模块及使用方法

### 5.1 ComponentScanAppModule

将扫描得到的 **ComponentAttribute** 注册到 autofac 的容器中去。
申明该模块时，需要指定需要扫描的模块。
大部分情况下，该模块是作为依赖项定义在别的 **IAppModule** 中
```csharp
// MyAppModule.cs
class MyAppModule : IAppModule
{
    public IEnumerable<IAppModule> DependentModules 
        => new IAppModule[]{ new ComponentScanAppModule(this) };
    
    public void OnUsing(AppSetup setup)
    {

    }
}
```

### 5.2 AutoConfigAppModule

注入配置模块。
申明该模块时，需要指定对哪个模块中的 **ConfigAttribute** 标识的类型进行配置的自动注入。
```csharp
// MyAppModule.cs
class MyAppModule : IAppModule
{
    public IEnumerable<IAppModule> DependentModules 
        => new IAppModule[]
        { 
            new ComponentScanAppModule(this), 
            new AutoConfigAppModule(this) 
        };
    
    public void OnUsing(AppSetup setup)
    {

    }
}
```

## 6 示例项目地址

```shell
git clone https://github.com/ShimizuShiori/Reface.AppStarter.Demo.git
```
