using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 记录类以及它的 Component 信息的数据结构
    /// </summary>
    public class TypeAndComponentInfo
    {
        public Type Type { get; private set; }
        public ComponentAttribute Attribute { get; private set; }

        public TypeAndComponentInfo(Type type, ComponentAttribute attribute)
        {
            Type = type;
            Attribute = attribute;
        }
    }
}
