using Reface.EventBus;

namespace Reface.AppStarter.Events
{
    public class AppStartedEvent : Event
    {
        public App App { get; private set; }

        public AppStartedEvent(object source, App app) : base(source)
        {
            this.App = app;
        }
    }
}
