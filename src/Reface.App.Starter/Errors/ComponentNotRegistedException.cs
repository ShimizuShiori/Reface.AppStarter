using System;

namespace Reface.AppStarter.Errors
{
    public class ComponentNotRegistedException : Exception
    {
        public Type RequiredType { get; private set; }

        public ComponentNotRegistedException(Type requiredType)
            :base($"[{requiredType}] 未注册")
        {
            RequiredType = requiredType;
        }
    }
}
