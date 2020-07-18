using Autofac;
using Reface.AppStarter.AutofacExt;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    [Obsolete("请使用 FactoryComponentRegistion")]
    public class CreatorComponentRegistion : IAutofacComponentRegistion
    {
        private readonly Type serviceType;
        private readonly Func<IComponentManager, object> creator;

        public CreatorComponentRegistion(Type serviceType,  Func<IComponentManager, object> creator)
        {
            this.serviceType = serviceType;
            this.creator = creator;
            this.ServiceTypes = new Type[] { this.serviceType };
        }

        public IEnumerable<Type> ServiceTypes { get; private set; }

        public string Key => null;

        public void RegisterToAutofac(ContainerBuilder builder, Type serviceType)
        {
            builder
                .Register(c => creator(new ComponentContextComponentManager(c)))
                .As(serviceType)
                .InstancePerLifetimeScope();
        }
    }
}
