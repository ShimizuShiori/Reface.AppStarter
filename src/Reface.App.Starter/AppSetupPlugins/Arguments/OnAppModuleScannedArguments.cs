using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter.AppSetupPlugins.Arguments
{
    public class OnAppModuleScannedArguments
    {
        public IAppModule AppModule { get; private set; }

        public IList<AttributeAndTypeInfo> AttributeAndTypeInfos { get; set; }

        public OnAppModuleScannedArguments(IAppModule appModule, IList<AttributeAndTypeInfo> attributeAndTypeInfos)
        {
            AppModule = appModule;
            AttributeAndTypeInfos = attributeAndTypeInfos;
        }
    }
}
