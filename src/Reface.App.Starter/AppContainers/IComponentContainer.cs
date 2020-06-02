using Reface.AppStarter.AutofacExt;
using System;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// DI容器的容器的接口类型
    /// </summary>
    public interface IComponentContainer : IAppContainer
    {
        /// <summary>
        /// 当组件被创建时的事件
        /// </summary>
        event EventHandler<ComponentCreatingEventArgs> ComponentCreating;

        /// <summary>
        /// 当外部申请一个组件，但该组件并未注册时的事件
        /// </summary>
        event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;

        event EventHandler<ComponentCreatedEventArgs> ComponentCreated;

        /// <summary>
        /// 创建一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateComponent<T>();

        /// <summary>
        /// 创建一个组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object CreateComponent(Type type);

        /// <summary>
        /// 创建一个具有独立生命周期的子容器
        /// </summary>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        IComponentContainer BeginScope(string scopeName);

        /// <summary>
        /// 对某个组件的属性进行注入
        /// </summary>
        /// <param name="value"></param>
        void InjectProperties(object value);
    }
}
