using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class CoreAppModule : IAppModule
    {
        public IEnumerable<IAppModule> DependentModules => null;

        public void OnUsing(AppSetup setup)
        {
        }
    }
}
