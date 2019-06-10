using System;

namespace Reface.AppStarter.Attributes
{
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
