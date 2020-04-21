using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.AppContainerBuilders
{
    public class ReplaceServiceContainerBuilder : IAppContainerBuilder
    {
        public class TargetAndMethodInfo
        {
            public object Target { get; private set; }
            public MethodInfo Method { get; private set; }

            public TargetAndMethodInfo(object target, MethodInfo method)
            {
                Target = target;
                Method = method;
            }
        }

        /// <summary>
        /// 记录已替换过的服务。
        /// 由于记录在不同 AppModule 上的特征是不同的实例，
        /// 因此将此属性设为静态
        /// </summary>
        private readonly Dictionary<string, IAppModule>
            replacedServiceToAppModuleMap = new Dictionary<string, IAppModule>();

        private readonly List<TargetAndMethodInfo> methods = new List<TargetAndMethodInfo>();

        public void TryRegister(IAppModule appModule, MethodInfo method)
        {
            if (method.ReturnType == typeof(void)) return;
            IAppModule appModuleHasBeenReplaced;
            if (replacedServiceToAppModuleMap.TryGetValue(method.ReturnType.FullName, out appModuleHasBeenReplaced))
                throw new ServiceHasBeenReplacedException(method.ReturnType, appModuleHasBeenReplaced.GetType());
            replacedServiceToAppModuleMap[method.ReturnType.FullName] = appModule;
            methods.Add(new TargetAndMethodInfo(appModule, method));
        }

        public void TryRegister(IAppModule appModule)
        {
            var scannedMethods = appModule.GetType().GetMethods()
                .Where(x => x.ReturnType != typeof(void))
                .Select(x => new { Method = x, Attribute = x.GetCustomAttribute<ReplaceCreatorAttribute>() })
                .Where(x => x.Attribute != null)
                .Select(x => x.Method);
            foreach (var method in scannedMethods)
            {
                this.TryRegister(appModule, method);
            }
        }

        public IAppContainer Build(AppSetup setup)
        {
            return EmptyAppContainer.Default;
        }

        public void Prepare(AppSetup setup)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.Building += AutofacContainerBuilder_Building;

        }

        private void AutofacContainerBuilder_Building(object sender, AppContainerBuilderBuildEventArgs e)
        {
            AutofacContainerBuilder autofacContainerBuilder = (AutofacContainerBuilder)sender;
            foreach (var method in methods)
            {
                autofacContainerBuilder.RemoveComponentByComponentType(method.Method.ReturnType);
                autofacContainerBuilder.RegisterByCreator(cm =>
                {
                    ParameterInfo[] ps = method.Method.GetParameters();
                    if (ps.Length == 0)
                        return method.Method.Invoke(method.Target, null);
                    object[] values = new object[ps.Length];
                    for (int i = 0; i < ps.Length; i++)
                    {
                        Type pType = ps[i].ParameterType;
                        object value = cm.CreateComponent(pType);
                        values[i] = value;
                    }
                    return method.Method.Invoke(method.Target, values);
                }, method.Method.ReturnType);
            }
        }
    }
}
