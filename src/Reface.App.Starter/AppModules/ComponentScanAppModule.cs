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
        public class MethodAndAttrInfo<TAttr> where TAttr : Attribute
        {
            public MethodInfo Method { get; private set; }
            public TAttr Attribute { get; private set; }

            public MethodAndAttrInfo(MethodInfo method, TAttr attribute)
            {
                Method = method;
                Attribute = attribute;
            }
        }

        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            RegisterScanResult(setup, targetModule, autofacContainerBuilder);
            RegisterComponentFromMethods(targetModule, autofacContainerBuilder);
            setup.AllModulesLoaded += (sender, e) =>
              {
                  ReplaceComponentFromMethods(targetModule, autofacContainerBuilder);
              };
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
                .Select(x => new MethodAndAttrInfo<ComponentCreatorAttribute>(x, x.GetCustomAttribute<ComponentCreatorAttribute>()))
                .Where(x => x.Attribute != null)
                .Select(x => x.Method);
            RegisterMethods(targetModule, autofacContainerBuilder, methods);
        }        /// <summary>
                 /// 从 <see cref="IAppModule"/> 实例的方法中创建组件，
                 /// 这些方法必须被标记 <see cref="ComponentAttribute"/> 特征。
                 /// </summary>
                 /// <param name="targetModule"></param>
                 /// <param name="autofacContainerBuilder"></param>
        private static void ReplaceComponentFromMethods(IAppModule targetModule, AutofacContainerBuilder autofacContainerBuilder)
        {
            var methods = targetModule.GetType().GetMethods()
                .Where(x => x.ReturnType != typeof(void))
                .Select(x => new MethodAndAttrInfo<ReplaceCreatorAttribute>(x, x.GetCustomAttribute<ReplaceCreatorAttribute>()))
                .Where(x => x.Attribute != null)
                .Select(x => x.Method);
            foreach (var method in methods)
            {
                autofacContainerBuilder.RemoveComponentByServiceType(method.ReturnType);
            }
            RegisterMethods(targetModule, autofacContainerBuilder, methods);
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
