using System;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public interface IComponentFactory
    {
        Type ServiceType { get; }

        string Key { get; }

        object Create(IComponentManager componentManager);
        bool IsSingleton { get; }
    }
}
