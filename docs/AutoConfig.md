# 使用 AutoConfigAppModule

**.Net** 自带的那套配置文件方法虽然很好用，但是无论是写 *Section* 还是定义 *.config* 文件都是非常繁琐的。

为了减化配置的过程，我设计将配置文件改为简单的 *json* 文件。

并简化配置加载的过程，直接将 json 中相应的节点反序列化给相应的类型，并将这些类型注册到 IOC / DI 容器中。

## 1 添加功能模块

想要使用这个功能，首先在你的 *AppModule* 上使用 *AutoConfigAppModule*

```csharp
[AutoConfigAppModule]
public class MyAppModule
{

}
```

## 2 编写配置类

为了规范文档结构，建议所有的 *Config* 全部放在 *Configs* 目录下
```shell
- youproject
    + Listeners # 事件监听器的目录
    + Commands # 命令目录 
    + CommandHandlers # 命令处理器目录
    + Services # 不用解释了
    - Configs
        MyConfig.cs # 自定义的配置类
```

该类型里只包含属性（没有方法），
属性建议加上默认值，以保证在没有配置时，该类型依然能够正常工作。
最后在类型上加上 *Config* 特征，
*Config* 特征需要一个字符作为它的构造参数，
这个字符就是在配置文件中的节点名称。

```csharp
[Config("MyNode")]
public class MyConfig
{
    public string SomeUrl { get; set; } = "www.github.com"
}
```

## 3 配置文件
配置文本是使用 *json* 编写的。
配置文件中的第一个 *json* 的属性就是 *Config* 中的构造参数。
它的值就是 *MyConfig* 的结构

```json
// myapp.json
{
    "MyNode" : {
        "SomeUrl" : "https://pages.github.com/"
    },
    "OtherConfig" : {
        "A" : 1,
        "B" : "bac",
        "C" : true
    }
}
```

这样，就可以将配置的值带入 *MyConfig* 了。

## 4 指定配置文件

配置文件路径的指定是在构造 *AppSetup* 时指定的。

```csharp
// 如果不提供配置文件路径，则以 ./app.json 作为默认值
AppSetup setup = new AppSetup("./myapp.json");
```