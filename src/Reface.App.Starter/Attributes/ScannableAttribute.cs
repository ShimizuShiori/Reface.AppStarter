using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 被标记了此特征的类型会被扫描，并允许在应用程序构建时获取这些信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ScannableAttribute : Attribute
    {
    }
}
