using Autofac;
using Reface.AppStarter.AutofacExt;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public class FactoryComponentRegistion : IAutofacComponentRegistion
    {
        private readonly IComponentFactory componentFactory;

        public FactoryComponentRegistion(IComponentFactory componentFactory)
        {
            this.componentFactory = componentFactory;
        }

        public string Key => componentFactory.Key;

        public IEnumerable<Type> ServiceTypes => new Type[] { componentFactory.ServiceType };

        public void RegisterToAutofac(ContainerBuilder builder, Type serviceType)
        {
            var r = builder
                .Register(c => componentFactory.Create(new ComponentContextComponentManager(c)))
                .As(serviceType);
            if (componentFactory.IsSingleton)
                r.SingleInstance();
            else
                r.InstancePerLifetimeScope();
        }
    }
}
