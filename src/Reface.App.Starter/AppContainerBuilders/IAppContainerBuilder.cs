using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// 应用程序容器构建器。
    /// 应用程序应当由多个不同样的容器组成，
    /// 而容器应当由容器构建器生成，
    /// 应用程序被装配时，只是对容器构建器设置，
    /// 最终生成 <see cref="App"/> 时，构建器自己会生成相应的容器
    /// </summary>
    public interface IAppContainerBuilder
    {
        /// <summary>
        /// 准备事件，在执行 Build 之前会调用的方法。
        /// 在从 <see cref="AppSetup"/> 构建成 <see cref="App"/> 时，
        /// 会调用托管的所有 <see cref="IAppContainerBuilder.Build(AppSetup)"/> 方法，来生成所有的 <see cref="IAppContainer"/> 实例。
        /// 但是执行顺序无法确定，有可能存在某个 <see cref="IAppContainerBuilder.Build(AppSetup)"/> 的过程中需要使用到其它 <see cref="IAppContainerBuilder"/> 中的内容，但同时无法知道要使用到的 <see cref="IAppContainerBuilder"/> 是否已经构建完成了。
        /// 因此加入了 <see cref="Prepare(AppSetup)"/> 阶段，建议在该阶段，所有的 <see cref="IAppContainerBuilder"/> 应当将其不依赖外部的事务准备完成，
        /// 在 <see cref="Build(AppSetup)"/> 阶段直接使用 <see cref="Prepare(AppSetup)"/> 得到的结果生成 <see cref="IAppContainer"/>。
        /// </summary>
        /// <param name="setup"></param>
        void Prepare(AppSetup setup);

        /// <summary>
        /// 构建应用程序容器
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        IAppContainer Build(AppSetup setup);
    }
}
