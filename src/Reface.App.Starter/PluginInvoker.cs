using Reface.AppStarter.AppSetupPlugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public class PluginInvoker<T>
    {
        private IEnumerable<IAppSetupPlugin> plugins;
        private T argument;

        public T Invoke(Action<IAppSetupPlugin, T> trigger)
        {
            foreach (var plugin in plugins)
            {
                trigger(plugin, this.argument);
            }
            return this.argument;
        }

        public static PluginsBuilder<T> SetArgument(T argument)
        {
            PluginInvoker<T> invoker = new PluginInvoker<T>();
            invoker.argument = argument;
            return new PluginsBuilder<T>(invoker);
        }

        public class PluginsBuilder<T>
        {
            private readonly PluginInvoker<T> invoker;

            public PluginsBuilder(PluginInvoker<T> invoker)
            {
                this.invoker = invoker;
            }

            public PluginInvoker<T> SetPlugins(IEnumerable<IAppSetupPlugin> plugins)
            {
                this.invoker.plugins = plugins;
                return this.invoker;
            }
        }
    }
}
