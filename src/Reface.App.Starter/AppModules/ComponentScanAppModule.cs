using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 组件扫描应用模块
    /// </summary>
    public class ComponentScanAppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            AppModuleScanResult appModuleScanResult
                = setup.GetScanResult(targetModule);
            appModuleScanResult
                .ScannableAttributeAndTypeInfos
                .Where(x => x.Attribute is ComponentAttribute)
                .Where(x => x.Type.IsClass && !x.Type.IsAbstract)
                .ForEach(x =>
                {
                    autofacContainerBuilder.Register(x.Type, ((ComponentAttribute)x.Attribute).RegistionMode);
                });
        }
    }
}
