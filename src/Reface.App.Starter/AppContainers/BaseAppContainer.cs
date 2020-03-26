namespace Reface.AppStarter.AppContainers
{
    public abstract class BaseAppContainer : IAppContainer
    {
        public virtual void Dispose()
        {
        }

        public virtual void OnAppPrepair(App app)
        {
        }

        public virtual void OnAppStarted(App app)
        {
        }
    }
}
