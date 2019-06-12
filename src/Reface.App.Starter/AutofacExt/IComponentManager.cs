using System;

namespace Reface.AppStarter.AutofacExt
{
    public interface IComponentManager
    {
        T CreateComponent<T>();
        object CreateComponent(Type type);
        void InjectPropeties(object value);
    }
}
