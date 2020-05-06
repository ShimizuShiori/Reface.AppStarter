using System;

namespace Reface.AppStarter.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class RegisterAsAttribute : Attribute
    {
        public Type ServiceType { get; private set; }

        public RegisterAsAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}
