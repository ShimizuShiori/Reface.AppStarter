using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.AppSetupPlugins.Arguments;

namespace Reface.AppStarter.AppSetupPlugins
{
    /// <summary>
    /// 在 <see cref="AppSetup"/> 工作周期中的插件
    /// </summary>
    public interface IAppSetupPlugin
    {
        /// <summary>
        /// 当一个 <see cref="IAppContainerBuilder"/> 被创建后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppContainerBuilderCreated(AppSetup setup, OnAppContainerBuilderCreatedArguments arguments);

        /// <summary>
        /// 当一个 <see cref="IAppModule"/> 被扫描完成后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments);

        void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments);

        /// <summary>
        /// 当 <see cref="IAppModule"/> 完成了 <see cref="IAppModule.OnUsing(AppSetup, IAppModule)"/> 后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments);
    }
}
