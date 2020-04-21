using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    public class DependentModuleFromAttributesBuiltEventArgs : EventArgs
    {
        public List<IAppModule> AppModules { get; private set; }

        public DependentModuleFromAttributesBuiltEventArgs(List<IAppModule> appModules)
        {
            AppModules = appModules;
        }
    }
}
