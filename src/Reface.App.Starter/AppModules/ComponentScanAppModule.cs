using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using System;
using System.Linq;
using System.Reflection;

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
            RegisterScanResult(setup, targetModule, autofacContainerBuilder);
            RegisterComponentFromMethods(targetModule, autofacContainerBuilder);
        }

        /// <summary>
        /// 从 <see cref="IAppModule"/> 实例的方法中创建组件，
        /// 这些方法必须被标记 <see cref="ComponentAttribute"/> 特征。
        /// </summary>
        /// <param name="targetModule"></param>
        /// <param name="autofacContainerBuilder"></param>
        private static void RegisterComponentFromMethods(IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder)
        {
            var methods = targetModule.GetType().GetMethods()
                .Select(x => new
                {
                    Method = x,
                    Attr = x.GetCustomAttribute<ComponentCreatorAttribute>()
                })
                .Where(x => x.Attr != null);
            foreach (var method in methods)
            {
                autofacContainerBuilder.RegisterByCreator(cm =>
                {
                    ParameterInfo[] ps = method.Method.GetParameters();
                    if (ps.Length == 0)
                        return method.Method.Invoke(targetModule, null);
                    object[] values = new object[ps.Length];
                    for (int i = 0; i < ps.Length; i++)
                    {
                        Type pType = ps[i].ParameterType;
                        object value = cm.CreateComponent(pType);
                        values[i] = value;
                    }
                    return method.Method.Invoke(targetModule, values);
                }, method.Method.ReturnType);
            }
        }

        /// <summary>
        /// 注册扫描的结果
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="targetModule">目标模块，A 使用 B，那 A 就是 targetModule</param>
        /// <param name="autofacContainerBuilder">autofac 容器构建器</param>
        private static void RegisterScanResult(AppSetup setup, IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder)
        {
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
