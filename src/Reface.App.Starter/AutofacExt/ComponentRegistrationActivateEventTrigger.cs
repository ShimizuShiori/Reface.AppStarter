using Autofac.Core;
using System;
using System.Diagnostics;

namespace Reface.AppStarter.AutofacExt
{
    public class ComponentRegistrationActivateEventTrigger
    {

        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;
        public event EventHandler<ComponentCreatedEventArgs> ComponentCreated;

        private readonly IComponentRegistration componentRegistration;
        private readonly Type serviceType;

        public ComponentRegistrationActivateEventTrigger(IComponentRegistration componentRegistration, Type serviceType)
        {
            this.componentRegistration = componentRegistration;
            this.serviceType = serviceType;

            this.componentRegistration.Activating += ComponentRegistration_Activating;
            this.componentRegistration.Activated += ComponentRegistration_Activated;
        }

        private void ComponentRegistration_Activated(object sender, ActivatedEventArgs<object> e)
        {
            this.ComponentCreated?.Invoke(this, new ComponentCreatedEventArgs(new ComponentContextComponentManager(e.Context), serviceType, e.Instance));
        }

        private void ComponentRegistration_Activating(object sender, ActivatingEventArgs<object> e)
        {
            IComponentRegistration registration = sender as IComponentRegistration;
            ComponentCreatingEventArgs moduleComponentCreatingEventArgs =
                new ComponentCreatingEventArgs(new ComponentContextComponentManager(e.Context), serviceType, e.Instance);
            Debug.WriteLine($"[{registration.Id}] ComponentCreating : {moduleComponentCreatingEventArgs}");
            this.ComponentCreating?.Invoke(this, moduleComponentCreatingEventArgs);
            if (moduleComponentCreatingEventArgs.IsReplaced)
                e.Instance = moduleComponentCreatingEventArgs.ReplacedObject;
        }
    }
}
