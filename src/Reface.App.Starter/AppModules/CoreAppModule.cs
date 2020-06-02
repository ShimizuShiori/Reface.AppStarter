using Reface.AppStarter.AppModulePrepairs;
using Reface.AppStarter.Attributes;
using Reface.CommandBus;
using Reface.EventBus;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 核心应用程序模块，即 Reface.AppStarter 代码库。
    /// 设置该模块，用于自动的将该库中的组件扫描进入构建器
    /// </summary>
    [AutoConfigAppModule]
    [ComponentScanAppModule]
    [AddDefaultPlugins]
    class CoreAppModule : AppModule
    {

        [ComponentCreator]
        public IEventBus GetEventBus(IEventListenerFinder eventListenerFinder)
        {
            return new DefaultEventBus(eventListenerFinder);
        }

        [ComponentCreator]
        public ICommandBus GetCommandBus(ICommandHandlerFactory commandHandlerFactory)
        {
            return new DefaultCommandBus(commandHandlerFactory);
        }
    }
}
