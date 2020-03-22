using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 应用模块接口，这是 Reface.AppStarter 框架的核心接口
    /// </summary>
    public interface IAppModule
    {
        /// <summary>
        /// 该模块所依赖的其它模块
        /// </summary>
        IEnumerable<IAppModule> DependentModules { get; }

        /// <summary>
        /// 当该模块被使用时
        /// </summary>
        /// <param name="setup">应用程序安装类</param>
        /// <param name="targetModule">使用 this 的模块信息</param>
        void OnUsing(AppSetup setup, IAppModule targetModule);
    }
}
