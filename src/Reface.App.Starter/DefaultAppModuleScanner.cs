using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;

namespace Reface.AppStarter
{
    /// <summary>
    /// 默认的程序集扫描器
    /// </summary>
    [RegisterAs(typeof(IAppModuleScanner))]
    public class DefaultAppModuleScanner : IAppModuleScanner
    {
        /// <summary>
        /// 缓存那些已经扫描过的结果，提高运行时的速度。
        /// </summary>
        private readonly Dictionary<string, IEnumerable<AttributeAndTypeInfo>>
            sacennedAssemlyNameToInfoCache = new Dictionary<string, IEnumerable<AttributeAndTypeInfo>>();

        public IEnumerable<AttributeAndTypeInfo> Scan(IAppModule appModule)
        {
            if (appModule == null)
                return new AttributeAndTypeInfo[] { };

            var assembly = appModule.GetType().Assembly;
            var assemblyName = assembly.FullName;
            IEnumerable<AttributeAndTypeInfo> result;

            if (sacennedAssemlyNameToInfoCache.TryGetValue(assemblyName, out result))
            {
                return result;
            }

            Type[] types = assembly.GetExportedTypes();
            IList<AttributeAndTypeInfo> scannableAttributeAndTypeInfos
                 = new List<AttributeAndTypeInfo>();
            foreach (Type type in types)
            {
                object[] objects = type.GetCustomAttributes(typeof(ScannableAttribute), true);
                if (objects.Length == 0) continue;

                foreach (var obj in objects)
                {
                    AttributeAndTypeInfo attributeAndTypeInfo
                        = new AttributeAndTypeInfo(obj as ScannableAttribute, type);
                    scannableAttributeAndTypeInfos.Add(
                        attributeAndTypeInfo
                    );

                }
            }

            sacennedAssemlyNameToInfoCache[assemblyName] = scannableAttributeAndTypeInfos;
            return scannableAttributeAndTypeInfos;
        }
    }
}
