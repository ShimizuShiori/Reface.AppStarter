using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter.ConfigRegistions
{
    /// <summary>
    /// 通过 <see cref="ConfigAttribute"/> 注册到容器的注册器
    /// </summary>
    public class ScannedConfigRegistion : IConfigRegistion
    {
        private readonly ConfigAttribute configAttribute;
        private readonly Type objectType;

        public ScannedConfigRegistion(AttributeAndTypeInfo info)
        {
            this.configAttribute = (ConfigAttribute)info.Attribute;
            this.objectType = info.Type;
        }

        public string Section => this.configAttribute.Section;

        public Type Type => this.objectType;

        public object CreateDefaultInstance()
        {
            return Activator.CreateInstance(this.objectType);
        }
    }
}
