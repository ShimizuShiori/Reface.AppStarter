using Reface.AppStarter.Attributes;
using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    [Config("Test2")]
    public class Test2Config
    {
        [Description("程序工作模式")]
        public Modes Mode { get; set; } = Modes.Tst;
    }
}
