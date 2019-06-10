using Autofac;
using System;
using Reface.EventBus;
using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppContainerBuilders
{
    public class AutofacContainerBuilder
        : NotifyBuildEventAppContainerBuilder
    {
        /// <summary>
        /// autofac原本的容器构建器实例
        /// </summary>
        public ContainerBuilder AutofacContainerBuilderInstance
        {
            get;
            private set;
        } = new ContainerBuilder();

        public AutofacContainerBuilder()
        {
            this.Register(typeof(DefaultEventBus));
        }

        public void Register(Type componentType, RegistionMode registionMode = RegistionMode.AsInterfaces)
        {
            if (componentType.GetInterfaces().Length == 0)
            {
                // 若组件没有实现任何接口，则移除注册为接口的方式
                // 除移除后没有注册方式，则注册到自身上
                registionMode = EnumHelper.RemoveFlag(registionMode, RegistionMode.AsInterfaces);
                if (registionMode == RegistionMode.No) registionMode = RegistionMode.AsSelf;
            }
            if (componentType.IsGenericType)
            {
                var r = this.AutofacContainerBuilderInstance
                    .RegisterGeneric(componentType)
                    .InstancePerLifetimeScope();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsSelf))
                    r.AsSelf();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsInterfaces))
                    r.AsImplementedInterfaces();
            }
            else
            {
                var r = this.AutofacContainerBuilderInstance
                    .RegisterType(componentType)
                    .InstancePerLifetimeScope();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsSelf))
                    r.AsSelf();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsInterfaces))
                    r.AsImplementedInterfaces();
            }
        }

        public void RegisterInstance(Object value)
        {
            this.AutofacContainerBuilderInstance
                .RegisterInstance(value)
                .AsSelf()
                .SingleInstance();
        }

        public override IAppContainer BuildAppContainer(AppSetup appSetup)
        {
            return new AutofacContainerComponentContainer(this.AutofacContainerBuilderInstance);
        }
    }
}
