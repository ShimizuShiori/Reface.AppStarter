using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.Tests.ModuleA.Services
{
    public interface IServiceA
    {
        void DoA();
    }

    [Component]
    public class ServiceA : IServiceA
    {
        public void DoA()
        {
            Console.WriteLine("A");
        }
    }
}
