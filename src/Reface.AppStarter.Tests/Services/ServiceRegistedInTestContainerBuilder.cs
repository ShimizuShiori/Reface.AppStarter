using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Tests.Services
{
    [Component]
    public class ServiceRegistedInTestContainerBuilder : IService
    {
        public ServiceRegistedInTestContainerBuilder()
        {
            Console.WriteLine("ServiceRegistedInTestContainerBuilder.Ctor");
        }
    }
}
