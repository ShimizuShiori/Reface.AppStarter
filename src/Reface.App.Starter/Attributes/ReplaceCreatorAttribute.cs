using Reface.AppStarter.AppModuleMethodHandlers;
using System;
using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 替换组件方法，使用该特征标记在一个 <see cref="AppModule"/> 内的方法上。
    /// 可以删除一个已注册的组件，并使用该方法提供的实例注册。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ReplaceCreatorAttribute : AppModuleMethodAttribute
    {
        public override Type AppModuleMethodHandlerType => typeof(ComponentReplaceHandler);
    }
}
