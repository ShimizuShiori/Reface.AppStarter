using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// class with this attribute will be scanned
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ScannableAttribute : Attribute
    {
    }
}
