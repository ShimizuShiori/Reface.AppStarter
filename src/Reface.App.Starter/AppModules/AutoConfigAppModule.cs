﻿using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 自动配置应用模块。
    /// 依赖该模块可以得到以下功能：
    /// 1、自动注册标有 <see cref="ConfigAttribute"/> 的类型为配置类；
    /// 2、将 <see cref="AppSetup"/> 中指定的配置文件内容反序列化到配置类中；
    /// 3、将所有被赋值过的配置类，以实例的形式注册到 <see cref="IComponentContainer"/> 中。
    /// 该类型实现了 <see cref="INamespaceFilter"/> 接口，因此你可以通过设置该接口的属性来缩小扫描的范围。
    /// </summary>
    public class AutoConfigAppModule : AppModule, INamespaceFilter
    {
        public string[] IncludeNamespaces { get; set; }
        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments arguments)
        {
            var setup = arguments.AppSetup;
            var targetModule = arguments.TargetAppModule;

            ConfigAppContainerBuilder builder = setup.GetAppContainerBuilder<ConfigAppContainerBuilder>();
            arguments.ScannedAttributeAndTypeInfos
                .Where(x => x.Attribute is ConfigAttribute)
                .ForEach(x => builder.AutoConfig(x));
        }
    }
}
