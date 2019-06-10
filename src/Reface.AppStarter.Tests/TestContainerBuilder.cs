using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using System;

namespace Reface.AppStarter.Tests
{
    public class TestContainerBuilder : IAppContainerBuilder
    {
        public IAppContainer Build(AppSetup setup)
        {
            throw new NotImplementedException();
        }

        public void Prepare(AppSetup setup)
        {
        }
    }
}
