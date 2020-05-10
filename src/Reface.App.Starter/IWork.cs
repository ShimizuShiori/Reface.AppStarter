using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public interface IWork : IDisposable
    {
        App App { get; }

        Dictionary<string, object> Context { get; }

        string WorkName { get; }

        IWork BeginWork(string workName);
        T CreateComponent<T>();
        object CreateComponent(Type type);

        void InjectProperties(object value);

    }
}
