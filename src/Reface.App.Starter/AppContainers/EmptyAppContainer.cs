namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// 空容器，有些 <see cref="AppContainerBuilders.IAppContainerBuilder"/> 是不需要生成容器的，此时返回空容器即可
    /// </summary>
    public class EmptyAppContainer : IAppContainer
    {
        private static readonly EmptyAppContainer def = new EmptyAppContainer();

        private EmptyAppContainer()
        {

        }

        public static EmptyAppContainer Default
        {
            get
            {
                return def;
            }
        }

        public void Dispose()
        {
        }

        public void OnAppPrepair(App app)
        {
        }

        public void OnAppStarted(App app)
        {
        }
    }
}
