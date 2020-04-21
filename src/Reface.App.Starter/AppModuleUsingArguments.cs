using Reface.AppStarter.AppModules;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// <see cref="AppModule.OnUsing(AppModuleUsingArguments)"/> 的参数
    /// </summary>
    public class AppModuleUsingArguments
    {
        /// <summary>
        /// 略
        /// </summary>
        public AppSetup AppSetup { get; private set; }

        /// <summary>
        /// 当 A 依赖 B 时，此处就是 B
        /// </summary>
        public IAppModule UsingAppModule { get; private set; }

        /// <summary>
        /// 当 A 依赖 B 时，此处就是 A
        /// </summary>
        public IAppModule TargetAppModule { get; private set; }

        /// <summary>
        /// 被扫描到的类型信息，这些类型属于 <see cref="TargetAppModule"/>
        /// </summary>
        public IEnumerable<AttributeAndTypeInfo> ScannedAttributeAndTypeInfos { get; set; }

        public AppModuleUsingArguments(AppSetup appSetup, IAppModule usingAppModule, IAppModule targetAppModule, IEnumerable<AttributeAndTypeInfo> scannedAttributeAndTypeInfos)
        {
            AppSetup = appSetup;
            this.UsingAppModule = usingAppModule;
            TargetAppModule = targetAppModule;
            ScannedAttributeAndTypeInfos = scannedAttributeAndTypeInfos;
        }

        public override string ToString()
        {
            return $"{UsingAppModule.GetType().Name}.Using(setup,{TargetAppModule?.GetType().Name ?? "Null"})";
        }
    }
}
