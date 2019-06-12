using Autofac;
using Reface.AppStarter.AutofacExt;
using Reface.AppStarter.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.AppContainers
{
    public class AutofacContainerComponentContainer : IComponentContainer
    {
        public IContainer Container { get; private set; }
        private readonly TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource;

        public AutofacContainerComponentContainer(ContainerBuilder containerBuilder, TriggerComponentCreatingEventAutofacSource triggerComponentCreatingEventAutofacSource)
        {
            this.triggerComponentCreatingEventAutofacSource = triggerComponentCreatingEventAutofacSource;
            this.Container = containerBuilder.Build();
        }

        public IComponentContainer BeginScope(string scopeName)
        {
            return new LifetimescopeComponentContainer(this.Container.BeginLifetimeScope(scopeName));
        }

        public T CreateComponent<T>()
        {
            return this.Container.Resolve<T>();
        }

        public object CreateComponent(Type type)
        {
            return this.Container.Resolve(type);
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
            this.triggerComponentCreatingEventAutofacSource.ComponentCreating += (sender, e) =>
            {
                IEventBus eventBus = e.ComponentManager.CreateComponent<IEventBus>();
                eventBus.Publish(new ComponentCreatingEvent(this, e));
            };
        }

        public void InjectProperties(object value)
        {
            this.Container.InjectProperties(value);
        }
    }
}
