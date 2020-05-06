using Reface.AppStarter.AppModules;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 收集所有 <see cref="IAppModule"/> 类型的收集器
    /// </summary>
    public interface IAllAppModuleTypeCollector
    {
        /// <summary>
        /// 收集
        /// </summary>
        /// <param name="rootAppModules">根 <see cref="IAppModule"/> 实例集合</param>
        /// <returns></returns>
        IEnumerable<Type> Collect(IEnumerable<IAppModule> rootAppModules);
    }
}
