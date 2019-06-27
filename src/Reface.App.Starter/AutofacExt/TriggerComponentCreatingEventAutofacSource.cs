using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Reface.AppStarter.AppContainers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AutofacExt
{
    public class TriggerComponentCreatingEventAutofacSource : IRegistrationSource
    {
        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;
        public event EventHandler<NoComponentRegistedEventArgs> NoComponentRegisted;

        public bool IsAdapterForIndividualComponents => false;

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {

            var swt = service as IServiceWithType;
            if (swt == null)
                return Enumerable.Empty<IComponentRegistration>();

            var registrations = registrationAccessor(service);
            if (registrations.Any())
            {
                foreach (var registration in registrationAccessor(service))
                {
                    registration.Metadata["ServiceType"] = swt.ServiceType;
                    registration.Activating += Registration_Activating;
                }
                return Enumerable.Empty<IComponentRegistration>();
            }
            NoComponentRegistedEventArgs noComponentRegistedEventArgs = new NoComponentRegistedEventArgs(swt.ServiceType);
            this.NoComponentRegisted?.Invoke(this, noComponentRegistedEventArgs);
            if (noComponentRegistedEventArgs.ComponentProvider == null)
                return Enumerable.Empty<IComponentRegistration>();
            var newRegistration = new ComponentRegistration(
               Guid.NewGuid(),
               new DelegateActivator(swt.ServiceType, (c, p) =>
               {
                   return noComponentRegistedEventArgs.ComponentProvider(new ComponentContextComponentManager(c));
               }),
                new CurrentScopeLifetime(),
                InstanceSharing.Shared,
                InstanceOwnership.OwnedByLifetimeScope,
                new[] { service },
                new Dictionary<string, object>()
          );
            return new IComponentRegistration[] { newRegistration }; 
        }

        private void Registration_Activating(object sender, ActivatingEventArgs<object> e)
        {
            Type serviceType = (sender as IComponentRegistration).Metadata["ServiceType"] as Type;
            ComponentCreatingEventArgs moduleComponentCreatingEventArgs =
                new ComponentCreatingEventArgs(new ComponentContextComponentManager(e.Context), serviceType, e.Instance);
            this.ComponentCreating?.Invoke(this, moduleComponentCreatingEventArgs);
            if (moduleComponentCreatingEventArgs.IsReplaced)
                e.Instance = moduleComponentCreatingEventArgs.ReplacedObject;

        }
    }
}
