using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 组件特征，标记了该特征的类型会被注入到 autofac 容器中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 组件注册模式
        /// </summary>
        public ComponentRegisterMode RegisterMode { get; }

        public ComponentAttribute(ComponentRegisterMode registerMode = ComponentRegisterMode.AsInterfaces)
        {
            RegisterMode = registerMode;
        }
    }
}
