using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.Configs
{
    [Config("Test")]
    public class TestConfig
    {
        public string Mode { get; set; }
    }
}
