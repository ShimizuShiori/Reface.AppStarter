using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using System;
using System.Reflection;

namespace Reface.AppStarter.AppModuleMethodHandlers
{
    public class ComponentCreatorHandler : IAppModuleMethodHandler
    {
        public void Handle(AppSetup appSetup, IAppModule appModule, MethodInfo method, Attribute attribute)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = appSetup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.RegisterMethodCreator(appModule, method);
        }
    }
}
