using Reface.AppStarter.AppModules;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AppSetupPlugins
{
    /// <summary>
    /// 对命名空间过滤的插件
    /// </summary>
    public class NamespaceFilterPlugin : AppSetupPlugin
    {
        public override void OnAppModuleBeforeUsing(AppSetup setup, AppModuleUsingArguments arguments)
        {
            if (!(arguments.UsingAppModule is INamespaceFilterer)) return;

            INamespaceFilterer filter = (INamespaceFilterer)arguments.UsingAppModule;

            IEnumerable<AttributeAndTypeInfo> infos = arguments.ScannedAttributeAndTypeInfos;
            if (filter.IncludeNamespaces != null && filter.IncludeNamespaces.Length > 0)
                infos = infos.Where(info =>
                {
                    string fullNameOfType = info.Type.FullName;
                    foreach (var ns in filter.IncludeNamespaces)
                    {
                        if (fullNameOfType.StartsWith(ns)) return true;
                    }
                    return false;
                });
            if (filter.ExcludeNamespaces != null && filter.ExcludeNamespaces.Length > 0)
                infos = infos.Where(info =>
                {
                    string fullNameOfType = info.Type.FullName;
                    foreach (var ns in filter.ExcludeNamespaces)
                    {
                        if (fullNameOfType.StartsWith(ns)) return false;
                    }
                    return true;
                });
            arguments.ScannedAttributeAndTypeInfos = infos.ToList();
        }
    }
}
