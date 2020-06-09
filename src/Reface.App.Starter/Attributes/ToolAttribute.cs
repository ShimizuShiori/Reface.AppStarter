using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 工具特征。
    /// 标有工具特征的类型会在 <see cref="AppSetup.CollectAllAppModuleType(System.Collections.Generic.IEnumerable{AppModules.IAppModule})"/> 后注册到 <see cref="AppSetup.Tools"/> 中。
    /// 并允许开发者从 <see cref="AppSetup.Tools"/> 中以接口的形式获取它们的实例。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ToolAttribute : Attribute
    {
    }
}
