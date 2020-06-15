using System.ComponentModel;
using System.Reflection;

namespace Reface.AppStarter
{
    public static class ReflectionExtensions
    {
        public static string GetDescription(this MemberInfo memberInfo)
        {
            var attr = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            return attr != null ? attr.Description : memberInfo.Name;
        }
    }
}
