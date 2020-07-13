using ConsoleApp1.Components;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Events;
using Reface.EventBus;
using System;

namespace ConsoleApp1.Listeners
{
    [Listener]
    public class ShowHelloOnAppStarted : IEventListener<AppStartedEvent>
    {
        private readonly ICmdController cmdController;

        public ShowHelloOnAppStarted(ICmdController cmdController)
        {
            this.cmdController = cmdController;
        }

        public void Handle(AppStartedEvent @event)
        {
            cmdController.ShowMessage(ConsoleColor.Green, "HelloWorld");
        }
    }
}
