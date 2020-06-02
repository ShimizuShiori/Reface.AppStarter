namespace Reface.AppStarter.ComponentLifetimeListeners
{
    /// <summary>
    /// 组件实现此接口可以监听被创建后的事件
    /// </summary>
    public interface IOnCreated
    {
        void OnCreated(CreateArguments arguments);
    }
}
