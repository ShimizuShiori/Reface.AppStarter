using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 表示组件在 <see cref="App"/> 内部是单例的
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class SingletonAttribute : Attribute
    {
    }
}
