using System;

namespace Reface.AppStarter
{
    public interface IComponentRegistion
    {
        void RegisterType<T>();
        void RegisterType(Type type);
        bool TryRegisterByComponentAttribute(Type type,out TypeAndComponentInfo typeAndComponentInfo);
    }
}
