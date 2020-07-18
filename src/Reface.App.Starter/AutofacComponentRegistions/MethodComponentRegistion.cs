using Autofac;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public class MethodComponentRegistion : IAutofacComponentRegistion
    {
        private readonly IAppModule appModule;
        private readonly MethodInfo methodInfo;
        private readonly bool isSingleton;

        public MethodComponentRegistion(IAppModule appModule, MethodInfo methodInfo)
        {
            this.appModule = appModule;
            this.methodInfo = methodInfo;

            this.isSingleton = this.methodInfo.GetCustomAttributes<SingletonAttribute>().Any();
        }

        public string Key => $"{methodInfo.DeclaringType.FullName}.{methodInfo.ToString()}";

        public IEnumerable<Type> ServiceTypes => new Type[] { methodInfo.ReturnType };

        public void RegisterToAutofac(ContainerBuilder builder, Type serviceType)
        {
            Func<IComponentManager, object> creator = cm =>
             {
                 ParameterInfo[] ps = methodInfo.GetParameters();
                 if (ps.Length == 0)
                     return methodInfo.Invoke(appModule, null);
                 object[] values = new object[ps.Length];
                 for (int i = 0; i < ps.Length; i++)
                 {
                     Type pType = ps[i].ParameterType;
                     object value = cm.CreateComponent(pType);
                     values[i] = value;
                 }
                 return methodInfo.Invoke(appModule, values);
             };
            var r = builder
                 .Register(c => creator(new ComponentContextComponentManager(c)))
                 .As(serviceType);

            if (isSingleton)
                r.SingleInstance();
            else
                r.InstancePerLifetimeScope();
        }
    }
}
