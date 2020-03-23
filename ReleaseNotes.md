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

# ?.?.?

* 新增 *Listener* 特征，用于 事件监听器 的特征