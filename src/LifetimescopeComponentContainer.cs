using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
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
    }
}
