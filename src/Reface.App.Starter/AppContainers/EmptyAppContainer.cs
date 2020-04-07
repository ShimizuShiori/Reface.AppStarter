namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// 空容器的实现类
    /// </summary>
    public class EmptyAppContainer : IAppContainer, IEmptyAppContainer
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
