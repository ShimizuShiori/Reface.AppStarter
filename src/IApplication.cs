using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序启动接口
    /// </summary>
    public interface IApplication : IModule
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="setup"></param>
        ApplicationEnvironment Start(ApplicationEnvironmentSetup setup);
        
    }
}
