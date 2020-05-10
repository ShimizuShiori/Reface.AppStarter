using Reface.AppStarter.AppSetupPlugins.Arguments;

namespace Reface.AppStarter.AppSetupPlugins
{
    /// <summary>
    /// 对 <see cref="IAppSetupPlugin"/> 的基本实现。
    /// 仅将接口要求的方法实现为虚方法，以便子类选择性重写。
    /// </summary>
    public abstract class AppSetupPlugin : IAppSetupPlugin
    {
        public virtual void OnAllAppModuleTypeCollected(AppSetup setup, OnAllAppModuleTypeCollectedArguments arguments)
        {
            
        }

        public virtual void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments)
        {
        }

        public virtual void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments)
        {
        }

        public virtual void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments)
        {
        }

        public virtual void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments)
        {
        }
    }
}
