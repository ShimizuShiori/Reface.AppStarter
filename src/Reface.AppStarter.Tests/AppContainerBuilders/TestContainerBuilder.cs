using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Tests.AppContainers;

namespace Reface.AppStarter.Tests.AppContainerBuilders
{
    public class TestContainerBuilder : IAppContainerBuilder
    {
        public void DoNothing() { }

        public IAppContainer Build(AppSetup setup)
        {
            return new TestContainer();
        }

        public void Prepare(AppSetup setup)
        {
        }
    }
}
