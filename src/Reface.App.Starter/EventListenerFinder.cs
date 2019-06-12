using Autofac;
using Reface.AppStarter.Attributes;
using Reface.EventBus;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    [Component]
    public class EventListenerFinder : IEventListenerFinder
    {
        private readonly ILifetimeScope lifetimeScope;

        public EventListenerFinder(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IEnumerable<IEventListener> CreateAllEventListeners()
        {
            return this.lifetimeScope.Resolve<IEnumerable<IEventListener>>();
        }
    }
}
