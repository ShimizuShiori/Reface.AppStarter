using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Reflection;

namespace Reface.AppStarter.AppModuleMethodHandlers
{
    public class ConfigCreatorHandler : IAppModuleMethodHandler
    {
        public void Handle(AppSetup appSetup, IAppModule appModule, MethodInfo method, Attribute attribute)
        {
            if (method.ReturnType == typeof(void)) return;

            ConfigAppContainerBuilder builder = appSetup.GetAppContainerBuilder<ConfigAppContainerBuilder>();

            ConfigCreatorAttribute config = (ConfigCreatorAttribute)attribute;
            builder.AddConfig(appModule, method);
        }
    }
}
