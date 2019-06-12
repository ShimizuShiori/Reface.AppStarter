using System;

namespace Reface.AppStarter.AppContainers
{
    public interface IAppContainer : IDisposable
    {
        void OnAppPrepair(App app);
        void OnAppStarted(App app);
    }
}
