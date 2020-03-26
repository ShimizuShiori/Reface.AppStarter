using Autofac;
using Autofac.Core.Registration;
using Reface.AppStarter.AutofacExt;
using Reface.AppStarter.Errors;
using Reface.AppStarter.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// autofac Di容器的容器
    /// </summary>
    public class AutofacContainerComponentContainer : IComponentContainer
    {
        public IContainer Container { get; private set; }
        private readonly ContainerBuilder containerBuilder;
        private readonly TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource;

        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;
        public event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;

        public AutofacContainerComponentContainer(ContainerBuilder containerBuilder, TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource)
        {
            this.containerBuilder = containerBuilder;
            this.triggerComponentCreatingEventAutofacSource = triggerComponentCreatingEventAutofacSource;
            this.triggerComponentCreatingEventAutofacSource.ComponentCreating += (sender, e) =>
            {
                this.ComponentCreating?.Invoke(this, e);
            };
            this.triggerComponentCreatingEventAutofacSource.NoComponentRegisted += (sender, e) =>
            {
                this.NoComponentRegisted?.Invoke(this, e);
            };
        }

        public IComponentContainer BeginScope(string scopeName)
        {
            return new LifetimescopeComponentContainer(this, this.Container.BeginLifetimeScope(scopeName));
        }

        public T CreateComponent<T>()
        {
            try
            {
                return this.Container.Resolve<T>();
            }
            catch (ComponentNotRegisteredException)
            {
                throw new ComponentNotRegistedException(typeof(T));
            }
        }

        public object CreateComponent(Type type)
        {
            try
            {
                return this.Container.Resolve(type);
            }
            catch (ComponentNotRegisteredException)
            {
                throw new ComponentNotRegistedException(type);
            }
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        public void OnAppStarted(App app)
        {
            using (var scope = this.Container.BeginLifetimeScope("OnAppStarted"))
            {
                IEventBus eventBus = scope.Resolve<IEventBus>();
                eventBus.Publish(new AppStartedEvent(this, app));
            }
        }

        public void InjectProperties(object value)
        {
            this.Container.InjectProperties(value);
        }

        public void OnAppPrepair(App app)
        {
            this.containerBuilder.RegisterInstance(app)
                .SingleInstance();
            this.Container = containerBuilder.Build();
        }
    }
}
