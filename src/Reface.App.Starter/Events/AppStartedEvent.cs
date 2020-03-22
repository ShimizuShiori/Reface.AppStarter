using Reface.EventBus;

namespace Reface.AppStarter.Events
{
    /// <summary>
    /// 应用程序启动事件，该事件需要使用事件总线来监听
    /// </summary>
    public class AppStartedEvent : Event
    {
        public App App { get; private set; }

        public AppStartedEvent(object source, App app) : base(source)
        {
            this.App = app;
        }
    }
}
