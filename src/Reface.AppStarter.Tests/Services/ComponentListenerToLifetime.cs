using Reface.AppStarter.Attributes;
using Reface.AppStarter.ComponentLifetimeListeners;

namespace Reface.AppStarter.Tests.Services
{
    public interface IComponentListenerToLifetime
    {
    }

    [Component]
    public class ComponentListenerToLifetime : IComponentListenerToLifetime, IOnCreated, IOnCreating
    {
        public void OnCreated(CreateArguments arguments)
        {
            var app = arguments.ComponentManager.CreateComponent<App>();
            app.Context["LastCreating"] = this.GetType();
        }

        public void OnCreating(CreateArguments arguments)
        {

            var app = arguments.ComponentManager.CreateComponent<App>();
            app.Context["LastCreated"] = this.GetType();
        }
    }
}
