namespace Reface.AppStarter.ComponentLifetimeListeners
{
    /// <summary>
    /// 组件实现此接口可以监听被创建时的事件
    /// </summary>
    public interface IOnCreating
    {
        void OnCreating(CreateArguments arguments);
    }
}
