using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 模块
    /// </summary>
    public interface IModule
    {
        IEnumerable<IModule> GetDependendModules(ApplicationEnvironmentSetup setup);

        Type ConfigType { get; }

        void OnUsing(ApplicationEnvironmentSetup setup);
        void OnUsed(ApplicationEnvironmentSetup setup);
        void OnComponentFound(ApplicationEnvironmentSetup setup, TypeAndComponentInfo typeAndComponentInfo);
        void OnApplicationStarted(ApplicationEnvironment environment, IComponentFactory componentFactory);
        void OnComponentCreating(ModuleComponentCreatingEventArgs eventArgs);
    }
}
