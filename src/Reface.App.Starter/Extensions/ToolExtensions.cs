using Reface.AppStarter.Attributes;
using System;
using System.Reflection;

namespace Reface.AppStarter
{
    public static class ToolExtensions
    {
        internal static bool IsTool(this Type type)
        {
            return type.GetCustomAttribute<ToolAttribute>() != null;
        }

        /// <summary>
        /// 对字段进行注入
        /// </summary>
        /// <param name="componentManager"></param>
        /// <param name="value"></param>
        public static void InjectFields(this IComponentManager componentManager, object value)
        {
            value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .ForEach(field =>
                {
                    object result;
                    if (!componentManager.TryCreateComponent(field.FieldType, out result))
                        return;

                    field.SetValue(value, result);
                });
        }
    }
}
