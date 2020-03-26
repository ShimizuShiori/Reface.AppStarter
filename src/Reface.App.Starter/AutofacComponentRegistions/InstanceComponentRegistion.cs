using Autofac;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public class InstanceComponentRegistion : IAutofacComponentRegistion
    {
        private readonly object instance;

        public InstanceComponentRegistion(object instance)
        {
            this.instance = instance;
            this.ServiceTypes = new Type[] { this.instance.GetType() };
        }

        public IEnumerable<Type> ServiceTypes { get; private set; }

        public void RegisterToAutofac(ContainerBuilder builder, Type serviceType)
        {
            builder
                .RegisterInstance(this.instance)
                .As(serviceType)
                .SingleInstance();
        }
    }
}
