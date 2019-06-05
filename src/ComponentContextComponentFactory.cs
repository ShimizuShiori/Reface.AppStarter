using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public class ComponentContextComponentFactory : IComponentFactory
    {
        private readonly IComponentContext componentContext;

        public ComponentContextComponentFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public IComponentFactory BeginScope(string name = "")
        {
            throw new NotImplementedException("该工厂无法创建作用域");
        }

        public T Create<T>()
        {
            return componentContext.Resolve<T>();
        }

        public object Create(Type type)
        {
            return componentContext.Resolve(type);
        }

        public void Dispose()
        {

        }

        public void InitProperties(object obj)
        {
            componentContext.InjectProperties(obj);
        }
    }
}
