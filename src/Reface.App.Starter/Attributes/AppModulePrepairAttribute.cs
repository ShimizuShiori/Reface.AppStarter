using System;
using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 模块准备组件。<br />
    /// 执行 <see cref="AppSetup.Start(AppModules.IAppModule)"/> 时，会先获取所有的 <see cref="IAppModule"/> 的类型（去重）。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class AppModulePrepairAttribute : Attribute
    {
        public abstract void Prepair(AppModulePrepareArguments args);
    }
}
