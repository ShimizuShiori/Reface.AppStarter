using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    public class AppModule : IAppModule
    {
        public virtual IEnumerable<IAppModule> DependentModules => null;

        public virtual void OnUsing(AppSetup setup)
        {
        }
    }
}
