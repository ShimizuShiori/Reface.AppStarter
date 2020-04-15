using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    [Description("某个不依赖 ConfigAttribute 的配置类")]
    public class SomeConfigWithoutAttribute
    {
        [Description("根ID")]
        public int RootId { get; set; }

        [Description("是否会生成负数")]
        public bool CanGenerateMinusNumber { get; set; }

        [Description("种子")]
        public int Seed { get; set; }

        [Description("玩家姓名")]
        public string PlayerName { get; set; }
    }
}
