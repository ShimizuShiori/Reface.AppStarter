using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.Tests.Listeners
{
    [Listener]
    public class ListenerComponentCreating : IEventListener<AppStartedEvent>
    {
        public void Handle(AppStartedEvent @event)
        {
            IComponentContainer componentContainer = @event.App.GetAppContainer<IComponentContainer>();
            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
            Console.WriteLine(this.GetType().FullName);
        }

        private void ComponentContainer_ComponentCreating(object sender, AutofacExt.ComponentCreatingEventArgs e)
        {
        }
    }
}
