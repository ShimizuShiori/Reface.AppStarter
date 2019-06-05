using Autofac;
using System;

namespace Reface.AppStarter
{
    public class AutofacContainerBuilder
        : IAppContainerBuilder
    {
        private readonly ContainerBuilder containerBuilder
            = new ContainerBuilder();

        public void Register(Type componentType, RegistionMode registionMode)
        {

        }

        public IAppContainer Build()
        {
            throw new NotImplementedException();
        }
    }
}
