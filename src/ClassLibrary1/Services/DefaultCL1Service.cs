using Reface.AppStarter.Attributes;

namespace ClassLibrary1.Services
{
    [Component]
    public class DefaultCL1Service : ICL1Service
    {
        public string GetName()
        {
            return "ClassLibrary1";
        }
    }
}
