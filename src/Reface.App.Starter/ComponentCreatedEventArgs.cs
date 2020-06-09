using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 组件创建完成后的事件
    /// </summary>
    public class ComponentCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// 组件管理器
        /// </summary>
        public IComponentManager ComponentManager { get; private set; }

        /// <summary>
        /// 需要创建时的类型
        /// </summary>
        public Type RequiredType { get; private set; }

        /// <summary>
        /// 已创建得到的对象
        /// </summary>
        public object CreatedObject { get; private set; }

        public ComponentCreatedEventArgs(IComponentManager componentManager, Type requiredType, object createdObject)
        {
            ComponentManager = componentManager;
            RequiredType = requiredType;
            CreatedObject = createdObject;
        }
    }
}
