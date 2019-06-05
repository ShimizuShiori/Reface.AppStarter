using System.Collections.Generic;

namespace Reface.AppStarter
{
    public class AppModuleScanResult
    {
        public IAppModule AppModule { get; private set; }
        public IEnumerable<AttributeAndTypeInfo> ScannableAttributeAndTypeInfos { get; private set; }

        public AppModuleScanResult(IAppModule appModule, IEnumerable<AttributeAndTypeInfo> scannableAttributeAndTypeInfos)
        {
            AppModule = appModule;
            ScannableAttributeAndTypeInfos = scannableAttributeAndTypeInfos;
        }
    }
}
