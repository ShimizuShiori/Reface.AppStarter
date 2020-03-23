using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 注册为事件监听器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ListenerAttribute : ComponentAttribute
    {
    }
}
