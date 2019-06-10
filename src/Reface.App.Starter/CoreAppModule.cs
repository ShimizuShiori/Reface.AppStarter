using System.Collections.Generic;

namespace Reface.AppStarter
{
    class CoreAppModule : IAppModule
    {
        public IEnumerable<IAppModule> DependentModules => null;

        public void OnUsing(AppSetup setup)
        {
        }
    }
}
