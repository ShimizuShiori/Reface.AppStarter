using Reface.AppStarter.AppModules;
using System;
using System.Reflection;

namespace Reface.AppStarter.AppModuleMethodHandlers
{
    /// <summary>
    /// 处理 <see cref="AppModule"/> 中方法的接口
    /// </summary>
    public interface IAppModuleMethodHandler
    {
        /// <summary>
        /// 处理过程
        /// </summary>
        /// <param name="appSetup"></param>
        /// <param name="appModule">扫到方法的 <see cref="AppModule"/> 实例</param>
        /// <param name="method">被扫到的方法</param>
        /// <param name="attribute">被扫的方法所拥有的特征</param>
        void Handle(AppSetup appSetup, IAppModule appModule, MethodInfo method, Attribute attribute);
    }
}
