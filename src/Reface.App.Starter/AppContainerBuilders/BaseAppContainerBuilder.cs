using Reface.AppStarter.AppContainers;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// 应用程序容器构建器的基础类。
    /// 将 <see cref="Prepare(AppSetup)"/> 作为抽方法，子类可根据需要重写。
    /// <see cref="Build(AppSetup)"/> 是抽象方法，子类必须重写
    /// </summary>
    public abstract class BaseAppContainerBuilder : IAppContainerBuilder
    {
        public abstract IAppContainer Build(AppSetup setup);

        public virtual void Prepare(AppSetup setup)
        {
        }
    }
}
