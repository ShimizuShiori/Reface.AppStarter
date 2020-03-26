using Reface.AppStarter.AppContainerBuilders;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Errors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 组件扫描应用模块
    /// </summary>
    public class ComponentScanAppModule : AppModule
    {
        /// <summary>
        /// 记录已替换过的服务。
        /// 由于记录在不同 AppModule 上的特征是不同的实例，
        /// 因此将此属性设为静态
        /// </summary>
        private static readonly Dictionary<string, IAppModule>
            replacedServiceToAppModuleMap = new Dictionary<string, IAppModule>();

        public static void CleanCachedData()
        {
            Console.WriteLine("ComponentScanAppModule.CleanCachedData");
            replacedServiceToAppModuleMap.Clear();
        }

        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            ReplaceServiceContainerBuilder replaceServiceContainerBuilder
                = setup.GetAppContainerBuilder<ReplaceServiceContainerBuilder>();

            replaceServiceContainerBuilder.TryRegister(targetModule);

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
                .Where(x => x.ReturnType != typeof(void))
                .Select(x => new { Method = x, Attribute = x.GetCustomAttribute<ComponentCreatorAttribute>() })
                .Where(x => x.Attribute != null)
                .Select(x => x.Method);
            RegisterMethods(targetModule, autofacContainerBuilder, methods);
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
                CheckCanRemove(targetModule, method);
                RemoveService(targetModule, autofacContainerBuilder, method);
            }
            RegisterMethods(targetModule, autofacContainerBuilder, methods);
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
            replacedServiceToAppModuleMap[method.ReturnType.FullName] = targetModule;
        }

        /// <summary>
        /// 检查是否可以替换服务
        /// </summary>
        /// <param name="targetModule"></param>
        /// <param name="method"></param>
        private static void CheckCanRemove(IAppModule targetModule, MethodInfo method)
        {
            IAppModule module;
            if (replacedServiceToAppModuleMap.TryGetValue(method.ReturnType.FullName, out module))
                throw new ServiceHasBeenReplacedException(method.ReturnType, targetModule.GetType());
        }

        /// <summary>
        /// 注册所有方法
        /// </summary>
        /// <param name="targetModule"></param>
        /// <param name="autofacContainerBuilder"></param>
        /// <param name="methods"></param>
        private static void RegisterMethods(IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder, System.Collections.Generic.IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                autofacContainerBuilder.RegisterByCreator(cm =>
                {
                    ParameterInfo[] ps = method.GetParameters();
                    if (ps.Length == 0)
                        return method.Invoke(targetModule, null);
                    object[] values = new object[ps.Length];
                    for (int i = 0; i < ps.Length; i++)
                    {
                        Type pType = ps[i].ParameterType;
                        object value = cm.CreateComponent(pType);
                        values[i] = value;
                    }
                    return method.Invoke(targetModule, values);
                }, method.ReturnType);
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
