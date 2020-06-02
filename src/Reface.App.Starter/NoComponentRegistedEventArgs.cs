using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 组件未被注册时的事件参数
    /// </summary>
    public class NoComponentRegistedEventArgs : EventArgs
    {
        /// <summary>
        /// 未组件的类型
        /// </summary>
        public Type ServiceType { get; private set; }

        /// <summary>
        /// 组件提供器。
        /// 一般情况下，拦截此事件希望能够提供一个未注册的实例作为服务的实现。
        /// 此属性委托要求返回 <see cref="ServiceType"/> 的实体类型，
        /// 参数中的 <see cref="IComponentManager"/> 能够让开发者向手动创建的实体注入属性。
        /// </summary>
        public Func<IComponentManager, object> ComponentProvider { get; set; }

        public NoComponentRegistedEventArgs(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
