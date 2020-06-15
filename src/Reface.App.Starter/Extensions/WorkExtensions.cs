using Reface.AppStarter.AppContainers;
using Reface.EventBus;

namespace Reface.AppStarter
{
    public static class WorkExtensions
    {
        /// <summary>
        /// 开启一个工作单元
        /// </summary>
        /// <param name="app"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static IWork BeginWork(this App app, string workName)
        {
            var container = app.GetAppContainer<IComponentContainer>();
            var scope = container.BeginScope(workName);
            return scope.CreateComponent<IWork>();

        }

        /// <summary>
        /// 发布一个事件
        /// </summary>
        /// <param name="work"></param>
        /// <param name="event"></param>
        public static void PublishEvent(this IWork work, Event @event)
        {
            IEventBus eventBus = work.CreateComponent<IEventBus>();
            eventBus.Publish(@event);
        }
    }
}
