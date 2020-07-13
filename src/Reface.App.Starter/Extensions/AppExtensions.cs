using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter
{
    public static class AppExtensions
    {
        /// <summary>
        /// 创建组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <returns></returns>
        public static T CreateComponent<T>(this App app)
        {
            IComponentContainer container = app.GetAppContainer<IComponentContainer>();
            return container.CreateComponent<T>();
        }
    }
}
