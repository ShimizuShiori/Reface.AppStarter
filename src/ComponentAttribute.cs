using System;

namespace Reface.AppStarter
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute
        : ScannableAttribute
    {
        public RegistionMode RegistionMode { get; private set; }

        public ComponentAttribute(RegistionMode registionMode = RegistionMode.AsInterfaces)
        {
            RegistionMode = registionMode;
        }
    }
}
