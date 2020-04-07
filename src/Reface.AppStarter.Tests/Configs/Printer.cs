using System.Collections.Generic;
using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    public class Printer
    {
        [Description("打印机类型")]
        public string PrintType { get; set; }

        [Description("待注入的值")]
        public IEnumerable<PropertyValue> Values { get; set; }
    }
}
