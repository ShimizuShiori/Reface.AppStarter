using System;
using System.Collections.Generic;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 应用程序模块实现类，这是一个类，同时也是一个特征。
    /// 它会自动将加载其特征上的模块成为依赖项
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AppModule : Attribute, IAppModule
    {
        private readonly List<IAppModule> dependentModules = new List<IAppModule>();

        public AppModule()
        {
            object[] attrs = this.GetType().GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                if (!(attr is AppModule)) continue;
                dependentModules.Add(attr as AppModule);
            }
            this.AppendOtherModules(this.dependentModules);
        }

        /// <summary>
        /// 实现于 <see cref="IAppModule.DependentModules"/>
        /// </summary>
        public virtual IEnumerable<IAppModule> DependentModules => dependentModules;

        /// <summary>
        /// 该方法会在反射 Attribute 模块后调用，允许开发者继续追加更多的模块。
        /// 请不要调用这个方法。
        /// 预计在 2.0 版本以后取消此方法，
        /// 改为通过事件通知 AppModulesOnAttributeLoaded 来让开发者可以额外定义 <see cref="AppModule"/>
        /// </summary>
        /// <param name="modules"></param>
        protected virtual void AppendOtherModules(List<IAppModule> modules)
        {

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
