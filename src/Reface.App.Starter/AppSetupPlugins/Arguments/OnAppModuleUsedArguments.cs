using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.AppSetupPlugins.Arguments
{
    public class OnAppModuleUsedArguments
    {
        public IAppModule AppModule { get; private set; }

        public OnAppModuleUsedArguments(IAppModule appModule)
        {
            AppModule = appModule;
        }
    }
}
