using Autofac;
using System;

namespace Reface.AppStarter.AppContainers
{
    public class LifetimescopeComponentContainer
        : IComponentContainer
    {
        public ILifetimeScope LifetimeScope { get; private set; }

        public LifetimescopeComponentContainer(ILifetimeScope lifetimeScope)
        {
            LifetimeScope = lifetimeScope;
        }

        public IComponentContainer BeginScope(string scopeName)
        {
            return new LifetimescopeComponentContainer(this.LifetimeScope.BeginLifetimeScope(scopeName));
        }

        public T CreateComponent<T>()
        {
            return this.LifetimeScope.Resolve<T>();
        }

        public object CreateComponent(Type type)
        {
            return this.LifetimeScope.Resolve(type);
        }

        public void Dispose()
        {
            this.LifetimeScope.Dispose();
        }

        public void OnAppStarted(App app)
        {
        }

        public void InjectProperties(object value)
        {
            this.LifetimeScope.InjectProperties(value);
        }
    }
}
