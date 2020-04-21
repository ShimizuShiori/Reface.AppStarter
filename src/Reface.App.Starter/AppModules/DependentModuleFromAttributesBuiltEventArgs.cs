using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// <see cref="AppModule.DependentModuleFromAttributesBuilt"/> 的事件参数
    /// </summary>
    public class DependentModuleFromAttributesBuiltEventArgs : EventArgs
    {
        /// <summary>
        /// 从 Attribute 上扫描到的 <see cref="AppModule"/>，你可以向其追加更多的 <see cref="IAppModule"/>
        /// </summary>
        public List<IAppModule> AppModules { get; private set; }

        public DependentModuleFromAttributesBuiltEventArgs(List<IAppModule> appModules)
        {
            AppModules = appModules;
        }
    }
}
