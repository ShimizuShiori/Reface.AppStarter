using Reface.AppStarter.AppSetupPlugins;
using System;
using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 向 <see cref="CoreAppModule"/> 添加的准备方法。
    /// 用于向 <see cref="AppSetup"/> 添加一些默认的 <see cref="IAppSetupPlugin"/> 。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    partial class AddDefaultPluginsAttribute : CustomAddPluginsAttribute
    {
        protected override IEnumerable<IAppSetupPlugin> CreatePlugins()
        {
            return new IAppSetupPlugin[]
            {
                new NamespaceFilterPlugin(),
                new AppModuleMethodPlugin()
            };
        }
    }
}
