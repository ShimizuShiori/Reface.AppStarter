using System;

namespace Reface.AppStarter.AppContainers
{
    public interface IAppContainer : IDisposable
    {
        void OnAppStarted(App app);
    }
}
