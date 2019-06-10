using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    public interface IAppModule
    {
        IEnumerable<IAppModule> DependentModules { get; }

        void OnUsing(AppSetup setup);
    }
}
