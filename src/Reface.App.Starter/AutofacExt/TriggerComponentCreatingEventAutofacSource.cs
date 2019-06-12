using Autofac.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AutofacExt
{
    public class TriggerComponentCreatingEventAutofacSource : IRegistrationSource
    {
        public event EventHandler<ComponentCreatingEventArgs> ComponentCreating;

        public bool IsAdapterForIndividualComponents => false;

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {

            var swt = service as IServiceWithType;
            if (swt == null)
                return Enumerable.Empty<IComponentRegistration>();

            foreach (var registration in registrationAccessor(service))
            {
                registration.Metadata["ServiceType"] = swt.ServiceType;
                registration.Activating += Registration_Activating;
            }
            return Enumerable.Empty<IComponentRegistration>();
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
