namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 核心应用程序模块，即 Reface.AppStarter 代码库。
    /// 设置该模块，用于自动的将该库中的组件扫描进入 构建器
    /// </summary>
    [AutoConfigAppModule]
    [ComponentScanAppModule]
    class CoreAppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            base.OnUsing(setup, targetModule);
        }
    }
}
