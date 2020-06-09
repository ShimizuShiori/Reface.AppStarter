using Reface.AppStarter.Attributes;

namespace Reface.AppStarter
{
    /// <summary>
    /// 使用构造函数 <see cref="AppSetup.AppSetup(AppSetupOptions)"/> 创建实例的参数。
    /// 开发者可以重定义配置文件路径以及全模块类型收集器的实例。
    /// </summary>
    public class AppSetupOptions
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFilePath { get; set; } //= "./app.json";
        /// <summary>
        /// 所有模块类型的收集器。
        /// 开发者可以重新定义该接口的实现类
        /// </summary>
        public IAllAppModuleTypeCollector AllAppModuleTypeCollector { get; set; }

        public AppSetupOptions()
        {
            this.ConfigFilePath = "./app.json";
            this.AllAppModuleTypeCollector = new DefaultAllAppModuleTypeCollectionor();
        }
    }
}
