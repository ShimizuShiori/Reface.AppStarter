using Reface.AppStarter.Attributes;
using Reface.EventBus;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    [Component]
    public class EventListenerFinder : IEventListenerFinder
    {
        private readonly Lazy<IEnumerable<IEventListener>> eventListeners;

        public EventListenerFinder(Lazy<IEnumerable<IEventListener>> eventListeners)
        {
            this.eventListeners = eventListeners;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.eventListeners.Value;
        }
    }
}
