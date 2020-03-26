using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Events;
using Reface.EventBus;

namespace Reface.AppStarter.Listeners
{
    [Listener]
    public class CleanComponentScanAppModuleCachedData : IEventListener<AppStartedEvent>
    {
        public void Handle(AppStartedEvent @event)
        {
            ComponentScanAppModule.CleanCachedData();
        }
    }
}
