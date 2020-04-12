using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    public enum Modes
    {
        [Description("开发环境")]
        Dev,
        [Description("测试环境")]
        Tst,
        [Description("生产环境")]
        Prd
    }
}
