using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.AppModuleMethodHandlers
{
    public class ComponentReplaceHandler : IAppModuleMethodHandler
    {
        public void Handle(AppSetup appSetup, IAppModule appModule, MethodInfo method, Attribute attribute)
        {
            var replaceBuilder = appSetup.GetAppContainerBuilder<ReplaceServiceContainerBuilder>();

            replaceBuilder.TryRegister(appModule, method);
        }
    }
}
