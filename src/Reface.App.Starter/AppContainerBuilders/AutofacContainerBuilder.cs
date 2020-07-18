﻿using Autofac;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.AutofacComponentRegistions;
using Reface.AppStarter.AutofacExt;
using Reface.AppStarter.Predicates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

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

        private readonly HashSet<string> existsRegistionKeys = new HashSet<string>();

        public AutofacContainerBuilder()
        {
            this.AutofacContainerBuilderInstance = new ContainerBuilder();
            this.AutofacContainerBuilderInstance.RegisterSource(triggerComponentCreatingEventAutofacSource);
        }

        private void RegisterAutofacComponentRegistion(Type serviceType, IAutofacComponentRegistion registion)
        {
            string key = $"{serviceType.FullName} : {registion.Key}";
            if (existsRegistionKeys.Contains(key))
            {
                Debug.WriteLine($"Component [{key}] Exists");
                return;
            }
            List<IAutofacComponentRegistion> registions;
            if (!serviceTypeToRegistionsMap.TryGetValue(serviceType, out registions))
            {
                registions = new List<IAutofacComponentRegistion>();
                serviceTypeToRegistionsMap[serviceType] = registions;
            }
            registions.Add(registion);
            existsRegistionKeys.Add(key);
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

        public void RemoveComponentByComponentType(Type componentType)
        {
            ComponentAttribute componentAttribute = componentType.GetCustomAttribute<ComponentAttribute>();
            Type[] interfaces = componentType.GetInterfaces();

            var hasInterface = Predicate.Create(() => interfaces.Length > 0);
            var doesNotHaveInterface = hasInterface.Not();
            var hasSelf = Predicate.Create(() => EnumHelper.HasFlag(componentAttribute.RegistionMode, RegistionMode.AsSelf));
            var hasInterfaces = Predicate.Create(() => EnumHelper.HasFlag(componentAttribute.RegistionMode, RegistionMode.AsInterfaces));
            var asSelf = doesNotHaveInterface.Or(hasSelf);

            if (asSelf.IsTrue())
            {
                this.RemoveComponentByServiceType(componentType);
                return;
            }

            foreach (var @interface in interfaces)
            {
                this.RemoveComponentByServiceType(@interface);
            }
        }

        public void Register(Type componentType, ComponentAttribute componentAttribute)
        {
            this.RegisterAutofacComponentRegistion(new ScannedComponentRegistion(componentType, componentAttribute.RegistionMode));
        }

        public void RegisterInstance(Object value)
        {
            this.RegisterAutofacComponentRegistion(new InstanceComponentRegistion(value));
        }

        public void RegisterMethodCreator(IAppModule appModule, MethodInfo method)
        {
            this.RegisterAutofacComponentRegistion(new MethodComponentRegistion(appModule, method));
        }

        public void RegisterComponentFactory(IComponentFactory componentFactory)
        {
            this.RegisterAutofacComponentRegistion(new FactoryComponentRegistion(componentFactory));
        }

        [Obsolete("请使用 RegisterComponentFactory")]
        public void RegisterByCreator(Func<IComponentManager, object> creator, Type serviceType)
        {
            this.RegisterAutofacComponentRegistion(new CreatorComponentRegistion(serviceType, creator));
        }

        public override IAppContainer BuildAppContainer(AppSetup appSetup)
        {
            foreach (var pair in this.serviceTypeToRegistionsMap)
            {
                Type serviceType = pair.Key;
                foreach (var registion in pair.Value)
                {
                    registion.RegisterToAutofac(this.AutofacContainerBuilderInstance, serviceType);
                }
            }
            return new AutofacContainerComponentContainer(this.AutofacContainerBuilderInstance, this.triggerComponentCreatingEventAutofacSource);
        }
    }
}
