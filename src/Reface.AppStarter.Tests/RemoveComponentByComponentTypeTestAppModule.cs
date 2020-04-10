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
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            AutofacContainerBuilder builder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            builder.Building += Builder_Building;

            ConfigAppContainerBuilder configBuilder = setup.GetAppContainerBuilder<ConfigAppContainerBuilder>();
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
