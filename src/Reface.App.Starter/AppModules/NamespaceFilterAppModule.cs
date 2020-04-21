using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 这是一个抽象的 <see cref="AppModule"/> 的子类。
    /// 它允许外部依赖一个模块的时候，限制它扫描的命名空间。
    /// 让其只关注规定的类型。
    /// 该类有两个属性，<see cref="IncludeNamespaces"/> 和 <see cref="ExcludeNamespaces"/>，
    /// 分别是包含的命名空间和排除的命名空间。
    /// 在过滤过程中，优先包含，再排除。
    /// 请注意，若你在 <see cref="IncludeNamespaces"/> 和 <see cref="ExcludeNamespaces"/> 中定义了相同的命名空间，
    /// 会因为先被包含、再被排除掉而失去这些组件。
    /// </summary>
    public abstract class NamespaceFilterAppModule : AppModule
    {
        /// <summary>
        /// 包含的命名空间，比如 A.B 将会包含 A.B.C 和 A.B.C.D。
        /// 不对此属性赋值表示包含程序集中的所有类型，再排除 <see cref="ExcludeNamespaces"/> 的部分
        /// </summary>
        public string[] IncludeNamespaces { get; set; }

        /// <summary>
        /// 排除的命名的空间，比如 A.B 将会排除 A.B.C 和 A.B.C.D
        /// </summary>
        public string[] ExcludeNamespaces { get; set; }

        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            var result = setup.GetScanResult(targetModule);
            var infos = result.ScannableAttributeAndTypeInfos;
            if (this.IncludeNamespaces != null && this.IncludeNamespaces.Length > 0)
                infos = infos.Where(info =>
                {
                    string fullNameOfType = info.Type.FullName;
                    foreach (var ns in this.IncludeNamespaces)
                    {
                        if (fullNameOfType.StartsWith(ns)) return true;
                    }
                    return false;
                });
            if (this.ExcludeNamespaces != null && this.ExcludeNamespaces.Length > 0)
                infos = infos.Where(info =>
                {
                    string fullNameOfType = info.Type.FullName;
                    foreach (var ns in this.ExcludeNamespaces)
                    {
                        if (fullNameOfType.StartsWith(ns)) return false;
                    }
                    return true;
                });
            this.OnScanResultFiltered(setup, targetModule, infos);
        }

        protected abstract void OnScanResultFiltered(AppSetup setup, IAppModule targetModule, IEnumerable<AttributeAndTypeInfo> attributeAndTypeInfos);
    }
}
