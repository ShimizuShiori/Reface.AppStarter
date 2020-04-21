using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Tests.ModuleA
{
    [ComponentScanAppModule(IncludeNamespaces = new string[] {
        "Reface.AppStarter.Tests.ModuleA"
    },
        ExcludeNamespaces = new string[] 
        { 
            "Reface.AppStarter.Tests.ModuleA.Dal"
        })]
    [AutoConfigAppModule(IncludeNamespaces = new string[] { "Reface.AppStarter.Tests.ModuleA" })]
    public class AAppModule : AppModule
    {
    }
}
