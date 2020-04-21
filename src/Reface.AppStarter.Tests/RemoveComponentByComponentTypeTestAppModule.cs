using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Tests.Configs;
using Reface.AppStarter.Tests.Services;

namespace Reface.AppStarter.Tests
{
    [ComponentScanAppModule]
    [AutoConfigAppModule]
    public class RemoveComponentByComponentTypeTestAppModule : AppModule
    {
        public override void OnUsing(AppModuleUsingArguments args)
        {
            AutofacContainerBuilder builder = args.AppSetup.GetAppContainerBuilder<AutofacContainerBuilder>();
            builder.Building += Builder_Building;

            ConfigAppContainerBuilder configBuilder = args.AppSetup.GetAppContainerBuilder<ConfigAppContainerBuilder>();
            configBuilder.ConfigObjectRegisted += ConfigBuilder_ConfigObjectRegisted;

        }

        private void ConfigBuilder_ConfigObjectRegisted(object sender, AppContainerBuilderBuildEventArgs e)
        {
            AutofacContainerBuilder builder = e.AppSetup.GetAppContainerBuilder<AutofacContainerBuilder>();
            builder.RemoveComponentByComponentType(typeof(Test2Config));
        }

        private void Builder_Building(object sender, AppContainerBuilderBuildEventArgs e)
        {
            var builder = (AutofacContainerBuilder)sender;
            builder.RemoveComponentByComponentType(typeof(ServiceRegistedInTestContainerBuilder));
        }
    }
}
