using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 命令总线模式下的命令接受者的特征类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandlerAttribute : ComponentAttribute
    {
    }
}
