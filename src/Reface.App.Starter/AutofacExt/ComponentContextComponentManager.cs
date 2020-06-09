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

        public void InjectProperties(object value)
        {
            this.componentContext.InjectProperties(value);
        }

        public bool TryCreateComponent(Type type, out object result)
        {
            return this.componentContext.TryResolve(type, out result);
        }

        public bool TryCreateComponent<T>(out T result) where T : class
        {
            return this.componentContext.TryResolve<T>(out result);
        }
    }
}
