using Autofac;
using Autofac.Core.Registration;
using Reface.AppStarter.Errors;
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
                this.ComponentCreating?.Invoke(this, e);

            this.parent.NoComponentRegisted += (sender, e) => this.NoComponentRegisted?.Invoke(this, e);

            this.parent.ComponentCreated += (sender, e) =>
              this.ComponentCreated?.Invoke(this, e);

            LifetimeScope = lifetimeScope;
        }

        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;
        public event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;
        public event EventHandler<ComponentCreatedEventArgs> ComponentCreated;

        public IComponentContainer BeginScope(string scopeName)
        {
            return new LifetimescopeComponentContainer(this, this.LifetimeScope.BeginLifetimeScope(scopeName));
        }

        public T CreateComponent<T>()
        {
            try
            {
                return this.LifetimeScope.Resolve<T>();
            }
            catch (ComponentNotRegisteredException)
            {
                throw new ComponentNotRegistedException(typeof(T));
            }
        }

        public object CreateComponent(Type type)
        {
            try
            {
                return this.LifetimeScope.Resolve(type);
            }
            catch (ComponentNotRegisteredException)
            {
                throw new ComponentNotRegistedException(type);
            }
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
