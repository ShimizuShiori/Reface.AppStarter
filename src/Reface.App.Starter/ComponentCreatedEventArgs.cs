using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 组件创建完成后的事件
    /// </summary>
    public class ComponentCreatedEventArgs : EventArgs
    {
        public IComponentManager ComponentManager { get; private set; }
        public Type RequiredType { get; private set; }
        public object CreatedObject { get; private set; }

        public ComponentCreatedEventArgs(IComponentManager componentManager, Type requiredType, object createdObject)
        {
            ComponentManager = componentManager;
            RequiredType = requiredType;
            CreatedObject = createdObject;
        }
    }
}
