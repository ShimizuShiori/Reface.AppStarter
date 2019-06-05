using System;

namespace Reface.AppStarter
{
    public class ConfigAttribute : Attribute
    {
        public string Name { get; private set; }

        public ConfigAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
