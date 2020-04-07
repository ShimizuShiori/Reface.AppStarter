using System.ComponentModel;

namespace Reface.AppStarter.Tests.Configs
{
    [Description("属性值对")]
    public class PropertyValue
    {
        [Description("属性名称")]
        public string Name { get; set; }
        [Description("属性值")]
        public string Value { get; set; }
    }
}
