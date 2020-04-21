using System.Collections.Generic;
using System;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 应用模块接口，这是 Reface.AppStarter 框架的核心接口。
    /// 你可以直接从 <see cref="AppModule"/> 继承来得到以 <see cref="Attribute"/> 的方式定义依赖关系。
    /// 你也可以从 <see cref="NamespaceFilterAppModule"/> 继承来得到对命名空间进行黑白名单过滤的功能。
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
