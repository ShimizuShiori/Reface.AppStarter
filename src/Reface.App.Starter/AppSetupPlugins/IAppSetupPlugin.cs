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
        /// 调用一个 <see cref="IAppModule.OnUsing(AppModuleUsingArguments)"/> 前的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments);

        /// <summary>
        /// 当 <see cref="IAppModule"/> 完成了 <see cref="IAppModule.OnUsing(AppModuleUsingArguments)"/> 后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments);

        /// <summary>
        /// 当 <see cref="AppSetup.ScanAppModule(IAppModule)"/> 完成后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAppModuleScanned(AppSetup setup, OnAppModuleScannedArguments arguments);

        /// <summary>
        /// 当收集到了系统中所有的 <see cref="IAppModule"/> 类型后的事件
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="arguments"></param>
        void OnAllAppModuleTypeCollected(AppSetup setup, OnAllAppModuleTypeCollectedArguments arguments);
    }
}
