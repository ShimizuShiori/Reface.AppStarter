using System;

namespace Reface.AppStarter.AppContainerBuilders
{
    public class AppContainerBuilderBuildEventArgs : EventArgs
    {
        public AppSetup AppSetup { get; private set; }

        public AppContainerBuilderBuildEventArgs(AppSetup appSetup)
        {
            AppSetup = appSetup;
        }
    }
}
