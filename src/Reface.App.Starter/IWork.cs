using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 工作单元。
    /// 每个工作单元内的组件都是单例的。
    /// 每个工作单元内可以访问同一个上下文实例。
    /// 每个工作单元可以开启新的工作单元。
    /// 每个工作单元可以创建实例，以及对已有实例的属性进行注入。
    /// </summary>
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
