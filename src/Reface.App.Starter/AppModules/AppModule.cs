using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 应用程序模块实现类，这是一个类，同时也是一个特征。
    /// 它会自动将加载其特征上的模块成为依赖项。
    /// 如果你的 <see cref="AppModule"/> 需要使用到目标模块中的扫描类型，建议从 <see cref="NamespaceFilterAppModule"/> 继承，<see cref="NamespaceFilterAppModule"/> 允许定义命名空间的黑白名单过滤器，可以提供更灵活的功能。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class AppModule : Attribute, IAppModule
    {
        private List<IAppModule> dependentModules;

        public event EventHandler<DependentModuleFromAttributesBuiltEventArgs> DependentModuleFromAttributesBuilt;

        private void BuildDependentModulesWhenItIsNull()
        {
            if (this.dependentModules != null) return;
            this.dependentModules = new List<IAppModule>();
            object[] attrs = this.GetType().GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (!(attr is AppModule)) continue;
                dependentModules.Add(attr as AppModule);
            }
            this.DependentModuleFromAttributesBuilt?.Invoke(this, new DependentModuleFromAttributesBuiltEventArgs(this.dependentModules));
        }

        public AppModule()
        {
        }

        /// <summary>
        /// 实现于 <see cref="IAppModule.DependentModules"/>
        /// </summary>
        public virtual IEnumerable<IAppModule> DependentModules
        {
            get
            {
                BuildDependentModulesWhenItIsNull();
                return this.dependentModules;
            }
        }

        /// <summary>
        /// 添加新的 <see cref="AppModule"/> 实例
        /// </summary>
        /// <param name="appModule"></param>
        /// <returns></returns>
        protected AppModule AddModule(IAppModule appModule)
        {
            this.dependentModules.Add(appModule);
            return this;
        }

        /// <summary>
        /// 实现于 <see cref="IAppModule.OnUsing(AppSetup, IAppModule)"/>
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="targetModule"></param>
        public virtual void OnUsing(AppSetup setup, IAppModule targetModule)
        {
        }
    }
}
