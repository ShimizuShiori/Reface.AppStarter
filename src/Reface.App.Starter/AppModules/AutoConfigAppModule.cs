using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    public class AutoConfigAppModule : IAppModule
    {
        public IEnumerable<IAppModule> DependentModules => null;

        private readonly IAppModule autoConfigAppModule;

        public AutoConfigAppModule(IAppModule autoConfigAppModule)
        {
            this.autoConfigAppModule = autoConfigAppModule;
        }

        public void OnUsing(AppSetup setup)
        {
            var result = setup.GetScanResult(this.autoConfigAppModule);
            ConfigAppContainerBuilder builder = setup.GetAppContainerBuilder<ConfigAppContainerBuilder>();
            result
                .ScannableAttributeAndTypeInfos
                .Where(x => x.Attribute is ConfigAttribute)
                .ForEach(x => builder.AutoConfig(x));
        }
    }
}
