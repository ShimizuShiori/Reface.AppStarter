using Autofac;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacComponentRegistions;
using Reface.AppStarter.AutofacExt;
using System;
using System.Collections.Generic;

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

        private readonly Dictionary<Type, List<IAutofacComponentRegistion>>
            serviceTypeToRegistionsMap = new Dictionary<Type, List<IAutofacComponentRegistion>>();

        public AutofacContainerBuilder()
        {
            this.AutofacContainerBuilderInstance = new ContainerBuilder();
            this.AutofacContainerBuilderInstance.RegisterSource(triggerComponentCreatingEventAutofacSource);
        }

        private void RegisterAutofacComponentRegistion(Type serviceType, IAutofacComponentRegistion registion)
        {
            List<IAutofacComponentRegistion> registions;
            if (!serviceTypeToRegistionsMap.TryGetValue(serviceType, out registions))
            {
                registions = new List<IAutofacComponentRegistion>();
                serviceTypeToRegistionsMap[serviceType] = registions;
            }
            registions.Add(registion);
        }
        private void RegisterAutofacComponentRegistion(IAutofacComponentRegistion registion)
        {
            foreach (var serviceType in registion.ServiceTypes)
                this.RegisterAutofacComponentRegistion(serviceType, registion);
        }

        public void RemoveComponentByServiceType(Type serviceType)
        {
            this.serviceTypeToRegistionsMap.Remove(serviceType);
        }

        public void Register(Type componentType, RegistionMode registionMode = RegistionMode.AsInterfaces)
        {
            this.RegisterAutofacComponentRegistion(new ScannedComponentRegistion(componentType, registionMode));
        }

        public void RegisterInstance(Object value)
        {
            this.RegisterAutofacComponentRegistion(new InstanceComponentRegistion(value));
        }

        [Obsolete("请使用 RegisterByCreator(Func<IComponentManager, object> creator, Type serviceType)")]
        public void RegisterByFunc(Type serviceType, Func<IComponentManager, object> creator)
        {
            this.RegisterByCreator(creator, serviceType);
        }

        public void RegisterByCreator(Func<IComponentManager, object> creator, Type serviceType)
        {
            this.RegisterAutofacComponentRegistion(new CreatorComponentRegistion(serviceType, creator));
        }

        public override void Prepare(AppSetup setup)
        {
            foreach (var pair in this.serviceTypeToRegistionsMap)
            {
                Type serviceType = pair.Key;
                foreach (var registion in pair.Value)
                {
                    registion.RegisterToAutofac(this.AutofacContainerBuilderInstance, serviceType);
                }
            }
        }

        public override IAppContainer BuildAppContainer(AppSetup appSetup)
        {
            return new AutofacContainerComponentContainer(this.AutofacContainerBuilderInstance, this.triggerComponentCreatingEventAutofacSource);
        }
    }
}
