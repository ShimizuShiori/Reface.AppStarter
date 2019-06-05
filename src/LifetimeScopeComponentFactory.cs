using Autofac;
using System;

namespace Reface.AppStarter
{
    public class LifetimeScopeComponentFactory : IComponentFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public LifetimeScopeComponentFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IComponentFactory BeginScope(string name = "")
        {
            return new LifetimeScopeComponentFactory(this.lifetimeScope.BeginLifetimeScope(name));
        }

        public T Create<T>()
        {
            return this.lifetimeScope.Resolve<T>();
        }

        public object Create(Type type)
        {
            return this.lifetimeScope.Resolve(type);
        }

        public void Dispose()
        {
            this.lifetimeScope.Dispose();
        }

        public void InitProperties(object obj)
        {
            this.lifetimeScope.InjectProperties(obj);
        }
    }
}
