using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.AppContainerBuilders;
using System.Collections.Generic;

namespace Reface.AppStarter.Tests
{
    public class TestAppModule : IAppModule
    {

        public IEnumerable<IAppModule> DependentModules => new IAppModule[]
        {
            new AutoConfigAppModule(this),
            new ComponentScanAppModule(this)
        };

        public void OnUsing(AppSetup setup)
        {
            var container = setup.GetAppContainerBuilder<TestContainerBuilder>();
            container.DoNothing();
        }
    }
}
