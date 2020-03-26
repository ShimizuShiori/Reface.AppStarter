using Autofac;
using System;
using Reface.EventBus;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacExt;
using Reface.CommandBus;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// autofac 容器构建器
    /// </summary>
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
        }

        private readonly TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource = new TriggerComponentCreatingEventAutofacSource();

        public AutofacContainerBuilder()
        {
            this.AutofacContainerBuilderInstance = new ContainerBuilder();
            this.AutofacContainerBuilderInstance.RegisterSource(triggerComponentCreatingEventAutofacSource);
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

        [Obsolete("请使用 RegisterByCreator(Func<IComponentManager, object> creator, Type serviceType)")]
        public void RegisterByFunc(Type serviceType, Func<IComponentManager, object> creator)
        {
            this.RegisterByCreator(creator, serviceType);
        }

        public void RegisterByCreator(Func<IComponentManager, object> creator, Type serviceType)
        {
            this.AutofacContainerBuilderInstance
                .Register(c => creator(new ComponentContextComponentManager(c)))
                .As(serviceType)
                .InstancePerLifetimeScope();
        }

        public override IAppContainer BuildAppContainer(AppSetup appSetup)
        {
            return new AutofacContainerComponentContainer(this.AutofacContainerBuilderInstance, this.triggerComponentCreatingEventAutofacSource);
        }
    }
}
