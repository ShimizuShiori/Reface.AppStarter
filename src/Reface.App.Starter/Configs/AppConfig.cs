using Reface.AppStarter.Attributes;
using System.ComponentModel;

namespace Reface.AppStarter.Configs
{
    [Description("面向 Reface.AppStarter 的配置")]
    [Config("App")]
    public class AppConfig
    {
        [Description("是否生成与配置文件相关的 JsonSchema 文件")]
        public bool GenerateConfigJsonSchema { get; set; } = true;
    }
}
