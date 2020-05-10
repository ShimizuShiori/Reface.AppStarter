using Reface.AppStarter.AppSetupPlugins;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 向 <see cref="AppSetup"/> 添加额外的插件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AddPluginsAttribute : CustomAddPluginsAttribute
    {
        public Type[] PluginTypes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginTypes">插件的类型，这些插件会通过无构造函数的方式创建</param>
        public AddPluginsAttribute(Type[] pluginTypes)
        {
            PluginTypes = pluginTypes;
        }

        protected override IEnumerable<IAppSetupPlugin> CreatePlugins()
        {
            if (this.PluginTypes == null) return new IAppSetupPlugin[] { };


            List<IAppSetupPlugin> result = new List<IAppSetupPlugin>();

            foreach (var type in this.PluginTypes)
            {
                result.Add((IAppSetupPlugin)Activator.CreateInstance(type));
            }

            return result;
        }
    }
}
