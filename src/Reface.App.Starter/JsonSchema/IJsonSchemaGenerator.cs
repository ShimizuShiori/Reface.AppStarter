using System.Collections.Generic;

namespace Reface.AppStarter.JsonSchema
{
    /// <summary>
    /// 根据已有的配置生成配置文件
    /// </summary>
    public interface IJsonSchemaGenerator
    {
        string Generate(IEnumerable<AttributeAndTypeInfo> configAttributeAndTypeInfos);
    }
}
