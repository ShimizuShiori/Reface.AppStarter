using Autofac;
using System;

namespace Reface.AppStarter.AutofacExt
{
    public class ComponentContextComponentManager : IComponentManager
    {
        private readonly IComponentContext componentContext;

        public ComponentContextComponentManager(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public T CreateComponent<T>()
        {
            return this.componentContext.Resolve<T>();
        }

        public object CreateComponent(Type type)
        {
            return this.componentContext.Resolve(type);
        }

        public void InjectPropeties(object value)
        {
            this.componentContext.InjectProperties(value);
        }
    }
}
