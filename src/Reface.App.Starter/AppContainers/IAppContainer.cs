using System;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// 应用容器，
    /// <see cref="App"/> 的实质就是 <see cref="IAppContainer"/> 的集合。
    /// 在一个 <see cref="App"/> 中的所有 <see cref="IAppContainer"/> 的类型是不相同的。
    /// </summary>
    public interface IAppContainer : IDisposable
    {
        /// <summary>
        /// 当 <see cref="App"/> 准备生成时的事件点。
        /// 由于 <see cref="App"/> 的生成过程无法确定 <see cref="IAppContainer"/> 的生成顺序，
        /// 如果遇到两个 <see cref="IAppContainer"/> 存在依赖关系，就不易处理。
        /// 因此，建议在 <see cref="IAppContainer.OnAppPrepair(App)"/> 阶段处理内部事务，
        /// 再在 <see cref="OnAppStarted(App)"/> 阶段处理与其它 <see cref="IAppContainer"/> 的事务。
        /// </summary>
        /// <param name="app"></param>
        void OnAppPrepair(App app);
        /// <summary>
        /// 当 <see cref="App"/> 生成的事件点。
        /// </summary>
        /// <param name="app"></param>
        void OnAppStarted(App app);
    }
}
