using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 应用模块接口，这是 Reface.AppStarter 框架的核心接口。
    /// 建议从 <see cref="AppModule"/> 继承，让你的 <see cref="IAppModule"/> 拥有丰富的功能。
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
        /// <param name="args"></param>
        void OnUsing(AppModuleUsingArguments args);
    }
}
