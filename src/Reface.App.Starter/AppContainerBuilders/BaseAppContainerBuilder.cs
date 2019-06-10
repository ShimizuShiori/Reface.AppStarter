using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppContainerBuilders
{
    public abstract class BaseAppContainerBuilder : IAppContainerBuilder
    {
        public abstract IAppContainer Build(AppSetup setup);

        public virtual void Prepare(AppSetup setup)
        {
        }
    }
}
