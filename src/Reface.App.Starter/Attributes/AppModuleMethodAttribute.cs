using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 为 <see cref="ComponentCreatorAttribute"/> 、<see cref="ConfigCreatorAttribute"/> 、<see cref="ReplaceCreatorAttribute"/> 等挂载在 <see cref="AppModules"/> 中方法的特征提供基本的实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AppModuleMethodAttribute : Attribute
    {
        /// <summary>
        /// 该特征的处理器类型。该类型必须实现 <see cref="IAppModuleMethodHandler"/> 接口
        /// </summary>
        /// <returns></returns>
        public abstract Type AppModuleMethodHandlerType { get; }
    }
}
