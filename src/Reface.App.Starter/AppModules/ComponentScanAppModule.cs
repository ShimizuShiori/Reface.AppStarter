using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 组件扫描应用模块
    /// </summary>
    public class ComponentScanAppModule : AppModule, INamespaceFilterer
    {
        public string[] IncludeNamespaces { get; set; }
        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppModuleUsingArguments arguments)
        {
            var setup = arguments.AppSetup;
            var targetModule = arguments.TargetAppModule;
            var infos = arguments.ScannedAttributeAndTypeInfos;

            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();

            RegisterScanResult(setup, targetModule, autofacContainerBuilder, infos);
        }


        /// <summary>
        /// 从 <see cref="IAppModule"/> 实例的方法中创建组件，
        /// 这些方法必须被标记 <see cref="ComponentAttribute"/> 特征。
        /// </summary>
        /// <param name="targetModule"></param>
        /// <param name="autofacContainerBuilder"></param>
        private static void ReplaceComponentFromMethods(IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder)
        {
            var methods = targetModule.GetType().GetMethods()
                .Where(x => x.ReturnType != typeof(void))
                .Select(x => new { Method = x, Attribute = x.GetCustomAttribute<ReplaceCreatorAttribute>() })
                .Where(x => x.Attribute != null)
                .Select(x => x.Method);
            foreach (var method in methods)
            {
                RemoveService(targetModule, autofacContainerBuilder, method);
            }
            //RegisterMethods(targetModule, autofacContainerBuilder, methods);
        }

        /// <summary>
        /// 移除服务
        /// </summary>
        /// <param name="targetModule"></param>
        /// <param name="autofacContainerBuilder"></param>
        /// <param name="method"></param>
        private static void RemoveService(IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder, MethodInfo method)
        {
            autofacContainerBuilder.RemoveComponentByServiceType(method.ReturnType);
        }

        /// <summary>
        /// 注册扫描的结果
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="targetModule">目标模块，A 使用 B，那 A 就是 targetModule</param>
        /// <param name="autofacContainerBuilder">autofac 容器构建器</param>
        private static void RegisterScanResult(AppSetup setup, IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder, IEnumerable<AttributeAndTypeInfo> infos)
        {
            infos.Where(x => x.Attribute is ComponentAttribute)
                .Where(x => x.Type.IsClass && !x.Type.IsAbstract)
                .ForEach(x =>
                {
                    autofacContainerBuilder.Register(x.Type, ((ComponentAttribute)x.Attribute).RegistionMode);
                });
        }

    }
}
