using Reface.AppStarter.AutofacExt;
using System;

namespace Reface.AppStarter.AppContainers
{
    public class NoComponentRegistedEventArgs : EventArgs
    {
        public Type ServiceType { get; private set; }

        public Func<IComponentManager, object> ComponentProvider { get; set; }

        public NoComponentRegistedEventArgs(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
