using System;

namespace Reface.AppStarter
{
    public interface IAppContainer : IDisposable
    {
        void OnAppStarted(App app);
    }
}
