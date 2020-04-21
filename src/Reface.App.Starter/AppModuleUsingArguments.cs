using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class AppModuleUsingArguments
    {
        public AppSetup AppSetup { get; private set; }

        public IAppModule UsingAppModule { get; private set; }
        public IAppModule TargetAppModule { get; private set; }
        public IEnumerable<AttributeAndTypeInfo> ScannedAttributeAndTypeInfos { get; set; }

        public AppModuleUsingArguments(AppSetup appSetup, IAppModule usingAppModule, IAppModule targetAppModule, IEnumerable<AttributeAndTypeInfo> scannedAttributeAndTypeInfos)
        {
            AppSetup = appSetup;
            this.UsingAppModule = usingAppModule;
            TargetAppModule = targetAppModule;
            ScannedAttributeAndTypeInfos = scannedAttributeAndTypeInfos;
        }

        public override string ToString()
        {
            return $"{UsingAppModule.GetType().Name}.Using(setup,{TargetAppModule?.GetType().Name ?? "Null"})";
        }
    }
}
