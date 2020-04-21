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
            autofacContainerBuilder.RegisterByCreator(cm =>
            {
                ParameterInfo[] ps = method.GetParameters();
                if (ps.Length == 0)
                    return method.Invoke(appModule, null);
                object[] values = new object[ps.Length];
                for (int i = 0; i < ps.Length; i++)
                {
                    Type pType = ps[i].ParameterType;
                    object value = cm.CreateComponent(pType);
                    values[i] = value;
                }
                return method.Invoke(appModule, values);
            }, method.ReturnType);
        }
    }
}
