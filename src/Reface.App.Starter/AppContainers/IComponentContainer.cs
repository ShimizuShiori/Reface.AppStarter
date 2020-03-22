using Reface.AppStarter.AutofacExt;
using System;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// autofac DI容器的容器
    /// </summary>
    public interface IComponentContainer : IAppContainer
    {
        event EventHandler<ComponentCreatingEventArgs> ComponentCreating;
        event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;
        T CreateComponent<T>();
        object CreateComponent(Type type);
        IComponentContainer BeginScope(string scopeName);
        void InjectProperties(object value);
    }
}
