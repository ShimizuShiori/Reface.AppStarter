namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 
    /// </summary>
    public interface INamespaceFilterer
    {
        /// <summary>
        /// 包含的命名空间，比如 A.B 将会包含 A.B.C 和 A.B.C.D。
        /// 不对此属性赋值表示包含程序集中的所有类型，再排除 <see cref="ExcludeNamespaces"/> 的部分
        /// </summary>
        string[] IncludeNamespaces { get; set; }

        /// <summary>
        /// 排除的命名的空间，比如 A.B 将会排除 A.B.C 和 A.B.C.D
        /// </summary>
        string[] ExcludeNamespaces { get; set; }
    }
}
