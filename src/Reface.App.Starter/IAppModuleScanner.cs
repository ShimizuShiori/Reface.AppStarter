using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public interface IAppModuleScanner
    {
        IEnumerable<AttributeAndTypeInfo> Scan(IAppModule appModule);
    }
}
