using System;
using System.Collections.Generic;
using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.AppSetupPlugins.Arguments
{
    /// <summary>
    /// <see cref="IAppSetupPlugin.OnAllAppModuleTypeCollected(AppSetup, OnAllAppModuleTypeCollectedArguments)"/> 的参数
    /// </summary>
    public class OnAllAppModuleTypeCollectedArguments
    {
        /// <summary>
        /// 所有的 <see cref="IAppModule"/> 类型（已去重）
        /// </summary>
        public IEnumerable<Type> AllAppModuleTypes { get; private set; }

        public OnAllAppModuleTypeCollectedArguments(IEnumerable<Type> allAppModuleTypes)
        {
            AllAppModuleTypes = allAppModuleTypes;
        }
    }
}
