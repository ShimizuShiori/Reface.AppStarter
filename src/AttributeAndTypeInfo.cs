using System;

namespace Reface.AppStarter
{
    public class AttributeAndTypeInfo
    {
        public ScannableAttribute Attribute { get; private set; }
        public Type Type { get; private set; }

        public AttributeAndTypeInfo(ScannableAttribute attribute, Type type)
        {
            Attribute = attribute;
            Type = type;
        }
    }
}
