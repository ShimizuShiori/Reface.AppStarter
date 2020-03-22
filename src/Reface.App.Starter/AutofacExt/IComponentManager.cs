using System;

namespace Reface.AppStarter.AutofacExt
{
    /// <summary>
    /// 组件管理器
    /// </summary>
    public interface IComponentManager
    {
        /// <summary>
        /// 创建组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateComponent<T>();

        /// <summary>
        /// 创建组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object CreateComponent(Type type);

        /// <summary>
        /// 对指定的类型中的所有属性进行注入
        /// </summary>
        /// <param name="value"></param>
        void InjectPropeties(object value);
    }
}
