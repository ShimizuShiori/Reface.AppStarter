using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Tests.ModuleB.Services
{
    public interface IServiceB
    {
        void DoB();
    }

    [Component]
    public class ServiceB : IServiceB
    {
        public void DoB()
        {
            Console.WriteLine("B");
        }
    }
}
