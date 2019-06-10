using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    public class ComponentScanAppModule : IAppModule
    {
        private readonly IAppModule targetModule;

        public IEnumerable<IAppModule> DependentModules => null;

        public ComponentScanAppModule(IAppModule targetModule)
        {
            this.targetModule = targetModule;
        }

        public void OnUsing(AppSetup setup)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            AppModuleScanResult appModuleScanResult
                = setup.GetScanResult(this.targetModule);
            appModuleScanResult
                .ScannableAttributeAndTypeInfos
                .Where(x => x.Attribute is ComponentAttribute)
                .ForEach(x =>
                {
                    autofacContainerBuilder.Register(x.Type, ((ComponentAttribute)x.Attribute).RegistionMode);
                });
        }
    }
}
