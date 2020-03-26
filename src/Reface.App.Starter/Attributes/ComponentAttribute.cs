using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// DI 组件，标记了此特征的类型会被注册到 autofac 的容器中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute
        : ScannableAttribute
    {

        public RegistionMode RegistionMode { get; private set; }

        public ComponentAttribute(RegistionMode registionMode = RegistionMode.AsInterfaces)
        {
            RegistionMode = registionMode;
        }
    }
}
