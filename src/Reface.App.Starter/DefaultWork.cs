using Autofac;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    [Component]
    public class DefaultWork : IWork
    {
        private readonly App app;
        private readonly ILifetimeScope lifetimeScope;

        public DefaultWork(App app, ILifetimeScope lifetimeScope)
        {
            this.app = app;
            this.lifetimeScope = lifetimeScope;
        }

        public string WorkName => this.lifetimeScope.Tag.ToString();

        public App App => this.app;

        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        public IWork BeginWork(string workName)
        {
            var scope = this.lifetimeScope.BeginLifetimeScope(workName);
            return new DefaultWork(this.app, scope);
        }

        public T CreateComponent<T>()
        {
            return this.lifetimeScope.Resolve<T>();
        }

        public object CreateComponent(Type type)
        {
            return this.lifetimeScope.Resolve(type);
        }

        public void Dispose()
        {
            this.lifetimeScope.Dispose();
        }

        public void InjectProperties(object value)
        {
            this.lifetimeScope.InjectProperties(value);
        }
    }
}
