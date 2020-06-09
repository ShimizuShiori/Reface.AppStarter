using System;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// DI容器的容器的接口类型
    /// </summary>
    public interface IComponentContainer : IAppContainer, IComponentManager
    {
        /// <summary>
        /// 当组件被创建时的事件
        /// </summary>
        event EventHandler<ComponentCreatingEventArgs> ComponentCreating;

        /// <summary>
        /// 当外部申请一个组件，但该组件并未注册时的事件
        /// </summary>
        event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;

        /// <summary>
        /// 当组件创建后的事件
        /// </summary>
        event EventHandler<ComponentCreatedEventArgs> ComponentCreated;

        /// <summary>
        /// 创建一个具有独立生命周期的子容器
        /// </summary>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        IComponentContainer BeginScope(string scopeName);
    }
}
