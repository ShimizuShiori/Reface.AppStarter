using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 表示该类型是一个与 AppSetup 构建时的配置文件相映射的一个类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute : ScannableAttribute
    {
        public string Section { get; private set; }

        public ConfigAttribute(string section)
        {
            Section = section;
        }
    }
}
