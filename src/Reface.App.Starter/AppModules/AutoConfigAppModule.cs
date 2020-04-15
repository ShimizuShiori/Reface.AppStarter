using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System.Linq;
using System.Reflection;
using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 自动配置应用模块。
    /// 依赖该模块可以得到以下功能：
    /// 1、自动注册标有 <see cref="ConfigAttribute"/> 的类型为配置类；
    /// 2、自动注册在 <see cref="AppModule"/> 内标有 <see cref="ConfigCreatorAttribute"/> 的无参数方法的返回类型为配置类；
    /// 3、将 <see cref="AppSetup"/> 中指定的配置文件内容反序列化到配置类中；
    /// 4、将所有被赋值过的配置类，以实例的形式注册到 <see cref="IComponentContainer"/> 中。
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

            targetModule.GetType()
                .GetMethods()
                .Where(x => x.GetParameters().Length == 0)
                .Where(x => x.ReturnType != typeof(void))
                .Where(x => x.GetCustomAttribute<ConfigCreatorAttribute>() != null)
                .ForEach(x => builder.AddConfig(targetModule, x));
        }
    }
}
