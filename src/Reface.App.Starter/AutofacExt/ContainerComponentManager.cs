﻿using Autofac;
using Reface.AppStarter.ComponentLifetimeListeners;
using System;

namespace Reface.AppStarter.AutofacExt
{
    /// <summary>
    /// 基于 <see cref="IContainer"/> 的组件管理器
    /// </summary>
    public class ContainerComponentManager : IComponentManager
    {
        private readonly IContainer container;

        public ContainerComponentManager(IContainer container)
        {
            this.container = container;
        }

        public T CreateComponent<T>()
        {
            return this.container.Resolve<T>();
        }

        public object CreateComponent(Type type)
        {
            return this.container.Resolve(type);
        }

        public void InjectProperties(object value)
        {
            this.container.InjectProperties(value);
            if (value is IOnPropertiesInjected opi) 
                opi.OnPropertiesInjected();
        }

        public bool TryCreateComponent(Type type, out object result)
        {
            return this.container.TryResolve(type, out result);
        }

        public bool TryCreateComponent<T>(out T result)
            where T : class
        {
            return this.container.TryResolve<T>(out result);
        }
    }
}
