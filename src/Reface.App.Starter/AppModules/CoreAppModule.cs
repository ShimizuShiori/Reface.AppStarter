using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    class CoreAppModule : IAppModule
    {
        public IEnumerable<IAppModule> DependentModules => null;

        public void OnUsing(AppSetup setup)
        {
        }
    }
}
