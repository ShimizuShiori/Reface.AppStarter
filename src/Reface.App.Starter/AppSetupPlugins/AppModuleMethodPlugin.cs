using Reface.AppStarter.AppModuleMethodHandlers;
using Reface.AppStarter.AppSetupPlugins.Arguments;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.AppStarter.AppSetupPlugins
{
    public class AppModuleMethodPlugin : AppSetupPlugin
    {
        private readonly Dictionary<Type, IAppModuleMethodHandler> attrToHandlerMap = new Dictionary<Type, IAppModuleMethodHandler>();

        public override void OnAppModuleUsed(AppSetup setup, OnAppModuleUsedArguments arguments)
        {
            foreach (var method in arguments.AppModule.GetType().GetMethods())
            {
                IEnumerable<AppModuleMethodAttribute> attrs = method.GetCustomAttributes<AppModuleMethodAttribute>();
                foreach (var attr in attrs)
                {
                    IAppModuleMethodHandler handler;
                    if (!attrToHandlerMap.TryGetValue(attr.GetType(), out handler))
                    {
                        handler = Activator.CreateInstance(attr.AppModuleMethodHandlerType) as IAppModuleMethodHandler;
                        if (handler == null)
                            throw new InvalidCastException($"无法将 {attr.AppModuleMethodHandlerType} 转换为 {typeof(IAppModuleMethodHandler)}");
                        attrToHandlerMap[attr.GetType()] = handler;
                    }
                    handler.Handle(setup, arguments.AppModule, method, attr);
                }
            }
        }
    }
}
