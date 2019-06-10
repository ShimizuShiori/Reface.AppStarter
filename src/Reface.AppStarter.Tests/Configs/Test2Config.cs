using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.Configs
{
    [Config("Test2")]
    public class Test2Config
    {
        public string Mode { get; set; } = "Test2";
    }
}
