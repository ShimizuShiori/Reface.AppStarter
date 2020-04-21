using Reface.AppStarter.AppContainerBuilders;

namespace Reface.AppStarter.AppSetupPlugins.Arguments
{
    public class OnAppContainerBuilderCreatedArguments
    {
        public IAppContainerBuilder AppContainerBuilder { get; private set; }

        public OnAppContainerBuilderCreatedArguments(IAppContainerBuilder appContainerBuilder)
        {
            AppContainerBuilder = appContainerBuilder;
        }
    }
}
