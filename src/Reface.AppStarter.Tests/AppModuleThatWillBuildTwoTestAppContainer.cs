using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.AppContainerBuilders;

namespace Reface.AppStarter.Tests
{
    public class AppModuleThatWillBuildTwoTestAppContainer : AppModule
    {
        public override void OnUsing(AppModuleUsingArguments args)
        {
            var builder1 = args.AppSetup.GetAppContainerBuilder<TestContainerBuilder>();
            var builder2 = args.AppSetup.GetAppContainerBuilder<TestContainerBuilder2>();
        }
    }
}
