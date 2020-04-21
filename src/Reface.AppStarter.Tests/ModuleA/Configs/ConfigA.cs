using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.ModuleA.Configs
{
    [Config("A")]
    public class ConfigA
    {
        public string Name { get; set; } = "A";
    }
}
