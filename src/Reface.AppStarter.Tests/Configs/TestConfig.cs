using Reface.AppStarter.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    [Config("Test")]
    public class TestConfig
    {
        [Description("工作模式")]
        public string Mode { get; set; } = "Mode";

        [Description("打印机")]
        public List<Printer> Printers { get; set; }

        [Description("Id集合")]
        public IEnumerable<int> IdList { get; set; }
    }
}
