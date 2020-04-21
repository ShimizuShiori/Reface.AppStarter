using Reface.AppStarter.AppSetupPlugins;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 向你的 <see cref="AppModule"/> 添加此接口，
    /// 可以让其拥有对扫描组件的过滤功能。
    /// 过滤的条件是通过 <see cref="IncludeNamespaces"/> 和 <see cref="ExcludeNamespaces"/> 设置的。
    /// 过滤的实现是通过 <see cref="NamespaceFilterPlugin" /> 完成的。
    /// 白名单先于黑名单处理，当白名单与黑名单拥有相同的内容时，会被移除。
    /// </summary>
    public interface INamespaceFilter
    {
        /// <summary>
        /// 包含的命名空间，比如 A.B 将会包含 A.B.C 和 A.B.C.D。
        /// 不对此属性赋值表示包含程序集中的所有类型，再排除 <see cref="ExcludeNamespaces"/> 的部分
        /// </summary>
        string[] IncludeNamespaces { get; }

        /// <summary>
        /// 排除的命名的空间，比如 A.B 将会排除 A.B.C 和 A.B.C.D
        /// </summary>
        string[] ExcludeNamespaces { get; }
    }
}
