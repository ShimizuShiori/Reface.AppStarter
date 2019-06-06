using Autofac;
using System;
using Reface.EventBus;

namespace Reface.AppStarter
{
    public class AutofacContainerBuilder
        : IAppContainerBuilder
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
            this.AutofacContainerBuilderInstance
                .RegisterType<DefaultEventBus>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public void Register(Type componentType, RegistionMode registionMode)
        {
            if (componentType.IsGenericType)
            {
                var r = this.AutofacContainerBuilderInstance
                    .RegisterGeneric(componentType)
                    .InstancePerLifetimeScope();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsInterfaces))
                    r.AsImplementedInterfaces();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsSelf))
                    r.AsSelf();
            }
            else
            {
                var r = this.AutofacContainerBuilderInstance
                    .RegisterType(componentType)
                    .InstancePerLifetimeScope();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsInterfaces))
                    r.AsImplementedInterfaces();
                if (EnumHelper.HasFlag(registionMode, RegistionMode.AsSelf))
                    r.AsSelf();
            }
        }

        public IAppContainer Build(AppSetup setup)
        {
            return new AutofacContainerComponentContainer(this.AutofacContainerBuilderInstance);
        }
    }
}
