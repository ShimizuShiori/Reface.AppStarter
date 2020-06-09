using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class DefaultAllAppModuleTypeCollectionor : IAllAppModuleTypeCollector
    {
        public IEnumerable<Type> Collect(IEnumerable<IAppModule> rootAppModules)
        {
            HashSet<Type> set = new HashSet<Type>();
            foreach (var appModule in rootAppModules)
                FillTypeSet(ref set, appModule);
            return set;
        }

        private void FillTypeSet(ref HashSet<Type> set, IAppModule appModule)
        {
            set.Add(appModule.GetType());
            if (appModule.DependentModules == null) return;
            foreach (var module in appModule.DependentModules)
                FillTypeSet(ref set, module);
        }
    }
}
