using Reface.AppStarter.AppContainers;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class App
    {
        private readonly IEnumerable<IAppContainer> appContainers;

        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        public App(IEnumerable<IAppContainer> appContainers)
        {
            this.appContainers = appContainers;
            appContainers.ForEach(x => x.OnAppPrepair(this));
            appContainers.ForEach(x => x.OnAppStarted(this));
        }

        public T GetAppContainer<T>()
            where T : IAppContainer
        {
            foreach (var container in this.appContainers)
            {
                if (container is T) return (T)container;
            }
            return default(T);
        }
    }
}
