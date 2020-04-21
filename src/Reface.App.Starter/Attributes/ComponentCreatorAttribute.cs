using Reface.AppStarter.AppModuleMethodHandlers;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 在 AppModule 中通过 Method 方法注册一个组件时，需要在方法上标记该特征。
    /// 标记了 <see cref="ComponentCreatorAttribute"/> 的方法必须有返回值，且返回类型就是注册的服务类型。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ComponentCreatorAttribute : AppModuleMethodAttribute
    {
        public override Type AppModuleMethodHandlerType => typeof(ComponentCreatorHandler);
    }
}
