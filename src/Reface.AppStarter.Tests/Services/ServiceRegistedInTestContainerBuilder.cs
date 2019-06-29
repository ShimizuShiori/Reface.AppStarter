using System;

namespace Reface.AppStarter.Tests.Services
{
    public class ServiceRegistedInTestContainerBuilder : IService
    {
        public ServiceRegistedInTestContainerBuilder()
        {
            Console.WriteLine("ServiceRegistedInTestContainerBuilder.Ctor");
        }
    }
}
