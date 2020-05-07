using Reface.AppStarter.AppModules;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reface.AppStarter.AppSetupPlugins.Arguments
{
    /// <summary>
    /// <see cref="IAppSetupPlugin.OnAppModuleScanned(AppSetup, OnAppModuleScannedArguments)"/> 的参数
    /// </summary>
    public class OnAppModuleScannedArguments
    {
        /// <summary>
        /// 所扫描的模块
        /// </summary>
        public IAppModule AppModule { get; private set; }

        /// <summary>
        /// 扫描到得的类型信息，只读集合。
        /// 开发者不允许修改集合内的成员。
        /// </summary>
        public IReadOnlyList<AttributeAndTypeInfo> AttributeAndTypeInfos { get; private set; }

        public OnAppModuleScannedArguments(IAppModule appModule, IEnumerable<AttributeAndTypeInfo> attributeAndTypeInfos)
        {
            AppModule = appModule;
            AttributeAndTypeInfos = new ReadOnlyCollection<AttributeAndTypeInfo>(attributeAndTypeInfos.ToList());
        }
    }
}
