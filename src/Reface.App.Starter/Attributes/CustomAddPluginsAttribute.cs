using Reface.AppStarter.AppSetupPlugins;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 以自定义的方式向 <see cref="AppSetup"/> 添加插件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class CustomAddPluginsAttribute : AppModulePrepairAttribute
    {
        public override void Prepair(AppModulePrepareArguments args)
        {
            foreach (var plugin in CreatePlugins())
                args.AppSetup.AddPlugin(plugin);
        }

        /// <summary>
        /// 自定义的创建插件的过程
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<IAppSetupPlugin> CreatePlugins();
    }
}
