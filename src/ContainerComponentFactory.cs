using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public class ContainerComponentFactory : IComponentFactory
    {
        private readonly IContainer container;

        public ContainerComponentFactory(IContainer container)
        {
            this.container = container;
        }

        public IComponentFactory BeginScope(string name = "")
        {
            return new LifetimeScopeComponentFactory(this.container.BeginLifetimeScope(name));
        }

        public T Create<T>()
        {
            return this.container.Resolve<T>();
        }

        public object Create(Type type)
        {
            return this.container.Resolve(type);
        }

        public void Dispose()
        {
            this.container.Dispose();
        }

        public void InitProperties(object obj)
        {
            this.container.InjectProperties(obj);
        }
    }
}
