using Reface.AppStarter.Attributes;
using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 特征与类型的信息
    /// </summary>
    public class AttributeAndTypeInfo
    {
        /// <summary>
        /// 类型上所指定的允许扫描的特征
        /// </summary>
        public ScannableAttribute Attribute { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; private set; }

        public AttributeAndTypeInfo(ScannableAttribute attribute, Type type)
        {
            Attribute = attribute;
            Type = type;
        }

        public override string ToString()
        {
            return $"{this.Attribute.GetType().Name} : {this.Type.Name}";
        }
    }
}
