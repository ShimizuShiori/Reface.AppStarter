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
        /// <summary>
        /// 唯一标识，相同的 <see cref="Key"/> 表示相同的注册器。
        /// <see cref="AppSetup"/> 会忽略后者的注册。<br />
        /// 若不提供 <see cref="Key"/> ( null )，则会跳过 <see cref="Key"/> 检查。
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 注册到哪些类型上
        /// </summary>
        IEnumerable<Type> ServiceTypes { get; }

        /// <summary>
        /// 注册到 Autofac 的过程
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceType"></param>
        void RegisterToAutofac(ContainerBuilder builder, Type serviceType);
    }
}
