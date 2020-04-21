using Reface.AppStarter.AppModules;
using System.Reflection;

namespace Reface.AppStarter
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
        /// <param name="appModule"></param>
        /// <param name="method"></param>
        void Handle(AppSetup appSetup, IAppModule appModule, MethodInfo method);
    }
}
