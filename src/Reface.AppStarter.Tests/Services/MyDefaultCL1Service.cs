using ClassLibrary1.Services;
using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.Services
{
    [Component]
    public class MyDefaultCL1Service : ICL1Service
    {
        public string GetName()
        {
            return "TEST";
        }
    }
}
