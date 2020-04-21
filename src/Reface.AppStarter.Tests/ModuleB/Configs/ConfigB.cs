using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.ModuleB.Configs
{
    [Config("B")]
    public class ConfigB
    {
        public string Name { get; set; } = "B";
    }
}
