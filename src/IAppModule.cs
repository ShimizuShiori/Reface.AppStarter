using System.Collections.Generic;

namespace Reface.AppStarter
{
    public interface IAppModule
    {
        IEnumerable<IAppModule> DependentModules { get; }

        void OnUsing(AppSetup setup);
    }
}
