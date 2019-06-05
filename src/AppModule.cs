using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class AppModule : IModule
    {
        public virtual Type ConfigType => null;

        public virtual IEnumerable<IModule> GetDependendModules(ApplicationEnvironmentSetup setup)
        {
            return new IModule[] { };
        }

        public virtual void OnApplicationStarted(ApplicationEnvironment environment, IComponentFactory componentFactory)
        {
        }

        public virtual void OnComponentCreating(ModuleComponentCreatingEventArgs eventArgs)
        {
        }

        public virtual void OnComponentFound(ApplicationEnvironmentSetup setup, TypeAndComponentInfo typeAndComponentInfo)
        {
        }

        public virtual void OnUsed(ApplicationEnvironmentSetup setup)
        {
        }

        public virtual void OnUsing(ApplicationEnvironmentSetup setup)
        {
        }
    }
}
