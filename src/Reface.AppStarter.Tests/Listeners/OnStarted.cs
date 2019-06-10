using Reface.AppStarter.Attributes;
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
            int i = @event.App.Context.GetOrCreate<int>("Index", key => 0);
            i++;
            @event.App.Context["Index"] = i;
            Console.WriteLine("app started");
        }
    }
}
