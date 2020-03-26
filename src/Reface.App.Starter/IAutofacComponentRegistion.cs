using Autofac;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 向 autofac 注册元件的注册器
    /// </summary>
    public interface IAutofacComponentRegistion
    {
        IEnumerable<Type> ServiceTypes { get; }
        void RegisterToAutofac(ContainerBuilder builder, Type serviceType);
    }
}
