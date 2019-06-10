using Reface.AppStarter.AppContainers;
using System;

namespace Reface.AppStarter.AppContainerBuilders
{
    public abstract class NotifyBuildEventAppContainerBuilder : BaseAppContainerBuilder, INotifyBuildEvent
    {
        public event EventHandler<AppContainerBuilderBuildEventArgs> Building;
        public event EventHandler<AppContainerBuilderBuildEventArgs> Built;

        public override IAppContainer Build(AppSetup setup)
        {
            AppContainerBuilderBuildEventArgs appContainerBuilderBuildEventArgs
                = new AppContainerBuilderBuildEventArgs(setup);
            this.Building?.Invoke(this, appContainerBuilderBuildEventArgs);
            IAppContainer appContainer = this.BuildAppContainer(setup);
            this.Built?.Invoke(this, appContainerBuilderBuildEventArgs);
            return appContainer;
        }

        public abstract IAppContainer BuildAppContainer(AppSetup appSetup);
        
    }
}
