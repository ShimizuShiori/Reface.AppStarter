using System;

namespace Reface.AppStarter
{
    public class ComponentCreatingEventArgs : EventArgs
    {
        /// <summary>
        /// 此时创建的需求类型
        /// </summary>
        public Type RequiredType { get; private set; }

        /// <summary>
        /// 已创建出的实例
        /// </summary>
        public object CreatedObject { get; private set; }

        /// <summary>
        /// 替换的对象
        /// </summary>
        public object ReplacedObject { get; private set; }

        /// <summary>
        /// 向外部告之是否对象已被替换
        /// </summary>
        public bool IsReplaced { get; private set; } = false;

        /// <summary>
        /// 组件管理器
        /// </summary>
        public IComponentManager ComponentManager { get; private set; }

        public ComponentCreatingEventArgs(IComponentManager componentManager, Type requiredType, object createdObject)
        {
            this.ComponentManager = componentManager;
            RequiredType = requiredType;
            CreatedObject = createdObject;
        }

        public void Replace(object newObject)
        {
            this.IsReplaced = true;
            this.ReplacedObject = newObject;
        }

        public override string ToString()
        {
            return $"{CreatedObject.GetType()} : {RequiredType}";
        }
    }
}