using Reface.AppStarter.Events;
using Reface.EventBus;
using System;

namespace Reface.AppStarter.Tests.Listeners
{
    [Component]
    public class OnStarted : IEventListener<AppStartedEvent>
    {
        public void Handle(AppStartedEvent @event)
        {
            Console.WriteLine("app started");
        }
    }
}
