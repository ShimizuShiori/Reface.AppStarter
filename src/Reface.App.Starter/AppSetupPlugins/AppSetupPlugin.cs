using Reface.AppStarter.AppSetupPlugins.Arguments;

namespace Reface.AppStarter.AppSetupPlugins
{
    /// <summary>
    /// 对 <see cref="IAppSetupPlugin"/> 的基本实现
    /// </summary>
    public abstract class AppSetupPlugin : IAppSetupPlugin
    {
        public virtual void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments)
        {
        }

        public virtual void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments)
        {
        }
        public virtual void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments)
        {
        }
    }
}
