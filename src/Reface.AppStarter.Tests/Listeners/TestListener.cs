using Reface.AppStarter.Attributes;
using Reface.AppStarter.Tests.Events;
using Reface.EventBus;

namespace Reface.AppStarter.Tests.Listeners
{
    [Listener]
    public class TestListener : IEventListener<TestEvent>
    {
        private readonly App app;
        private readonly IWork work;

        public TestListener(App app, IWork work)
        {
            this.app = app;
            this.work = work;
        }

        public void Handle(TestEvent @event)
        {
            app.Context["TEST"] = "TEST";
        }
    }
}
