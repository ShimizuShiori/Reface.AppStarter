using Reface.EventBus;

namespace Reface.AppStarter.Events
{
    /// <summary>
    /// 系统启动中事件，会发生在 <see cref="AppStartedEvent"/> 之前
    /// </summary>
    public class AppStartingEvent : Event
    {
        public App App { get; private set; }
        public AppStartingEvent(object source, App app) : base(source)
        {
            this.App = app;
        }
    }
}
