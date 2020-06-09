using Autofac;
using Autofac.Core.Registration;
using Reface.AppStarter.AutofacExt;
using Reface.AppStarter.ComponentLifetimeListeners;
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
        public event EventHandler<ComponentCreatedEventArgs> ComponentCreated;

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
            this.triggerComponentCreatingEventAutofacSource.ComponentCreated += (sender, e) =>
            {
                this.ComponentCreated?.Invoke(this, e);
            };
            this.ComponentCreating += TriggerOnCreating;
            this.ComponentCreated += TriggerOnCreated;
        }

        private void TriggerOnCreated(object sender, ComponentCreatedEventArgs e)
        {
            if (e.CreatedObject is IOnCreated listener)
                listener.OnCreated(new CreateArguments(e.ComponentManager, e.RequiredType));
        }

        private void TriggerOnCreating(object sender, ComponentCreatingEventArgs e)
        {
            if (e.CreatedObject is IOnCreating listener)
                listener.OnCreating(new CreateArguments(e.ComponentManager, e.RequiredType));
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

        public bool TryCreateComponent(Type type, out object result)
        {
            return this.Container.TryResolve(type, out result);
        }

        public bool TryCreateComponent<T>(out T result) where T : class
        {
            return this.Container.TryResolve<T>(out result);
        }
    }
}
