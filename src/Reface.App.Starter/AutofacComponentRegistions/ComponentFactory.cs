using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public class ComponentFactory : IComponentFactory
    {
        private readonly string key;
        private readonly Type serviceType;
        private readonly bool isSingleton;
        private Func<IComponentManager, object> creator;

        public ComponentFactory(string key, Type serviceType, Func<IComponentManager, object> creator, bool isSingleton)
        {
            this.key = key;
            this.serviceType = serviceType;
            this.creator = creator;
            this.isSingleton = isSingleton;
        }
        public Type ServiceType => serviceType;

        public string Key => key;

        public bool IsSingleton => isSingleton;

        public object Create(IComponentManager componentManager)
        {
            return creator(componentManager);
        }
    }
}
