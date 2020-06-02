using Reface.AppStarter.AppSetupPlugins;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 插件执行器。
    /// 对指定的插件集合进行执行，并构建参数和返回参数
    /// </summary>
    /// <typeparam name="T">待执行的插件方法中需要的参数</typeparam>
    public class PluginInvoker<T>
    {
        private IEnumerable<IAppSetupPlugin> plugins;
        private T argument;

        /// <summary>
        /// 执行所有插件
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public T Invoke(Action<IAppSetupPlugin, T> trigger)
        {
            foreach (var plugin in plugins)
            {
                trigger(plugin, this.argument);
            }
            return this.argument;
        }

        /// <summary>
        /// 构建插件执行的参数
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
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

            /// <summary>
            /// 设置所有的插件
            /// </summary>
            /// <param name="plugins"></param>
            /// <returns></returns>
            public PluginInvoker<T> SetPlugins(IEnumerable<IAppSetupPlugin> plugins)
            {
                this.invoker.plugins = plugins;
                return this.invoker;
            }
        }
    }
}
