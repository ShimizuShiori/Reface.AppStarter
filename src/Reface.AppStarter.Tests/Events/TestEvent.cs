using Reface.EventBus;

namespace Reface.AppStarter.Tests.Events
{
    public class TestEvent : Event
    {
        public TestEvent(object source) : base(source)
        {
        }
    }
}
