using Autofac;
using Reface.AppStarter.AutofacExt;
using System;

namespace Reface.AppStarter.AppContainers
{
    public class LifetimescopeComponentContainer
        : IComponentContainer
    {
        public ILifetimeScope LifetimeScope { get; private set; }
        private readonly IComponentContainer parent;

        public LifetimescopeComponentContainer(IComponentContainer parent, ILifetimeScope lifetimeScope)
        {
            this.parent = parent;
            this.parent.ComponentCreating += (sender, e) =>
            {
                this.ComponentCreating?.Invoke(this, e);
            };
            LifetimeScope = lifetimeScope;
        }

        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;

        public IComponentContainer BeginScope(string scopeName)
        {
            return new LifetimescopeComponentContainer(this, this.LifetimeScope.BeginLifetimeScope(scopeName));
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

        public void OnAppPrepair(App app)
        {
        }
    }
}
