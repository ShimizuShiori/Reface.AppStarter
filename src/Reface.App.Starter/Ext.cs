using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using Reface.EventBus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter
{
    /// <summary>
    /// 提供各种扩展方法
    /// </summary>
    public static class Ext
    {
        #region IEnumerable

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> handler)
        {
            if (list == null) return list;
            foreach (var item in list)
                handler(item);
            return list;
        }

        public static string Join(this IEnumerable<string> list, string joiner)
        {
            if (joiner == null) joiner = "";
            if (list == null) return "";
            int count = list.Count();
            if (count == 0) return "";
            if (count == 1) return list.First();
            return list.Aggregate((a, b) => $"{a}{joiner}{b}");
        }

        #endregion

        #region Dictionary

        public static TValue GetOrCreate<TValue>(this Dictionary<string, object> target, string key, Func<string, TValue> factory)
        {
            object objResult;
            if (target.TryGetValue(key, out objResult))
                return (TValue)objResult;
            TValue result;
            result = factory(key);
            target[key] = result;
            return result;
        }

        #endregion

        #region Reflection

        public static string GetDescription(this MemberInfo memberInfo)
        {
            var attr = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            return attr != null ? attr.Description : memberInfo.Name;
        }

        #endregion

        #region Work

        /// <summary>
        /// 开启一个工作单元
        /// </summary>
        /// <param name="app"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        public static IWork BeginWork(this App app, string workName)
        {
            var container = app.GetAppContainer<IComponentContainer>();
            var scope = container.BeginScope(workName);
            return scope.CreateComponent<IWork>();

        }

        /// <summary>
        /// 发布一个事件
        /// </summary>
        /// <param name="work"></param>
        /// <param name="event"></param>
        public static void PublishEvent(this IWork work, Event @event)
        {
            IEventBus eventBus = work.CreateComponent<IEventBus>();
            eventBus.Publish(@event);
        }

        #endregion

        #region Tools

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

        #endregion
    }
}
