using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用模块扫描结果
    /// </summary>
    public class AppModuleScanResult
    {
        /// <summary>
        /// 该结果所属的应用模块
        /// </summary>
        public IAppModule AppModule { get; private set; }

        /// <summary>
        /// 扫描的结果
        /// </summary>
        public IEnumerable<AttributeAndTypeInfo> ScannableAttributeAndTypeInfos { get; private set; }

        public AppModuleScanResult(IAppModule appModule, IEnumerable<AttributeAndTypeInfo> scannableAttributeAndTypeInfos)
        {
            AppModule = appModule;
            ScannableAttributeAndTypeInfos = scannableAttributeAndTypeInfos;
        }
    }
}
