using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppContainerBuilders
{
    public interface IAppContainerBuilder
    {
        void Prepare(AppSetup setup);

        IAppContainer Build(AppSetup setup);
    }
}
