using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.AppContainerBuilders;

namespace Reface.AppStarter.Tests
{
    [AutoConfigAppModule]
    [ComponentScanAppModule]
    public class TestAppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            var container = setup.GetAppContainerBuilder<TestContainerBuilder>();
            container.DoNothing();
        }
    }
}
