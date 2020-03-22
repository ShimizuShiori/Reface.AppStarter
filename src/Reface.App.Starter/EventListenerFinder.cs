using Autofac;
using Reface.AppStarter.Attributes;
using Reface.EventBus;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 事件监听者搜索器，为 EventBus 功能服务
    /// </summary>
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
