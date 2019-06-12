using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Events;
using Reface.EventBus;

namespace Reface.AppStarter.Tests.Listeners
{
    public class ListenerComponentCreating : IEventListener<AppStartedEvent>
    {
        public void Handle(AppStartedEvent @event)
        {
            IComponentContainer componentContainer = @event.App.GetAppContainer<IComponentContainer>();
            componentContainer.ComponentCreating += ComponentContainer_ComponentCreating;
        }

        private void ComponentContainer_ComponentCreating(object sender, AutofacExt.ComponentCreatingEventArgs e)
        {
        }
    }
}
