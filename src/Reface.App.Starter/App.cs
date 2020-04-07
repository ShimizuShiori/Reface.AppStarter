using Reface.AppStarter.AppContainers;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序运行时，
    /// 通过它可以访问应用程序级别的上下文，
    /// 可以获取指定的应用程序容器。
    /// </summary>
    public class App
    {
        private readonly IEnumerable<IAppContainer> appContainers;

        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        internal App(IEnumerable<IAppContainer> appContainers)
        {
            this.appContainers = appContainers.Where(x => !(x is IEmptyAppContainer));
            this.appContainers.ForEach(x => x.OnAppPrepair(this));
        }

        /// <summary>
        /// 获取指定类型的应用程序容器
        /// </summary>
        /// <typeparam name="T">容器类型，必须实现了 <see cref="Reface.AppStarter.AppContainers.IAppContainer"/> 接口</typeparam>
        /// <returns></returns>
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
