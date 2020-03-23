using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 自动配置应用模块
    /// </summary>
    public class AutoConfigAppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            if (targetModule == null) return;
            var result = setup.GetScanResult(targetModule);
            ConfigAppContainerBuilder builder = setup.GetAppContainerBuilder<ConfigAppContainerBuilder>();
            result
                .ScannableAttributeAndTypeInfos
                .Where(x => x.Attribute is ConfigAttribute)
                .ForEach(x => builder.AutoConfig(x));
        }
    }
}
