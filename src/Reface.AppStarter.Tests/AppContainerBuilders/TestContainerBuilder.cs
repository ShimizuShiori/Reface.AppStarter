﻿using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AutofacComponentRegistions;
using Reface.AppStarter.Tests.AppContainers;
using Reface.AppStarter.Tests.Services;

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
            AutofacContainerBuilder autofacContainerBuilder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.RegisterComponentFactory(new ComponentFactory
                (
                    "ServiceRegistedInTestContainerBuilder",
                    typeof(IService),
                    cm => new ServiceRegistedInTestContainerBuilder(),
                    false
                ));
        }
    }
}
