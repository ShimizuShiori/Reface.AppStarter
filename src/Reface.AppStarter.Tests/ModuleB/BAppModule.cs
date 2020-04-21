using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Tests.ModuleB
{
    [ComponentScanAppModule(IncludeNamespaces = new string[]
    {
        "Reface.AppStarter.Tests.ModuleB"
    })]
    [AutoConfigAppModule
        (
            IncludeNamespaces = new string[]
            {
                "Reface.AppStarter.Tests.ModuleB"
            }
        )]
    public class BAppModule : AppModule
    {
    }
}
