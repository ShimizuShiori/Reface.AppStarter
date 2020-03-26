using Autofac;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AutofacComponentRegistions
{
    public class ScannedComponentRegistion : IAutofacComponentRegistion
    {
        private readonly Type componentType;
        private readonly RegistionMode registionMode;

        public ScannedComponentRegistion(Type componentType, RegistionMode registionMode)
        {
            this.componentType = componentType;
            this.registionMode = registionMode;

            var mode = this.registionMode;
            if (componentType.GetInterfaces().Length == 0)
            {
                // 若组件没有实现任何接口，则移除注册为接口的方式
                // 除移除后没有注册方式，则注册到自身上
                mode = EnumHelper.RemoveFlag(registionMode, RegistionMode.AsInterfaces);
                if (mode == RegistionMode.No) mode = RegistionMode.AsSelf;
            }

            List<Type> serviceTypes = new List<Type>();
            if (EnumHelper.HasFlag(mode, RegistionMode.AsInterfaces))
                serviceTypes.AddRange(componentType.GetInterfaces());
            if (EnumHelper.HasFlag(mode, RegistionMode.AsSelf))
                serviceTypes.Add(componentType);
            this.ServiceTypes = serviceTypes;

        }

        public IEnumerable<Type> ServiceTypes { get; private set; }

        public void RegisterToAutofac(ContainerBuilder builder, Type serviceType)
        {
            if (componentType.IsGenericType)
            {
                var r = builder
                    .RegisterGeneric(componentType)
                    .As(serviceType)
                    .InstancePerLifetimeScope();
            }
            else
            {
                var r = builder
                    .RegisterType(componentType)
                    .As(serviceType)
                    .InstancePerLifetimeScope();
            }
        }
    }
}
