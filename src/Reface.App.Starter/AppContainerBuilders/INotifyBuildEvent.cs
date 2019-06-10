using System;

namespace Reface.AppStarter.AppContainerBuilders
{
    public interface INotifyBuildEvent
    {
        event EventHandler<AppContainerBuilderBuildEventArgs> Building;
        event EventHandler<AppContainerBuilderBuildEventArgs> Built;
    }
}
