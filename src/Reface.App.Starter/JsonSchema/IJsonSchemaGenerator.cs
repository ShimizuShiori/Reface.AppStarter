using System.Collections.Generic;
using Reface.AppStarter.Errors;

namespace Reface.AppStarter.JsonSchema
{
    /// <summary>
    /// 根据已有的配置生成配置文件
    /// </summary>
    public interface IJsonSchemaGenerator
    {
        /// <summary>
        /// 根据给定的配置类信息，生成 JsonSchema 的文本。
        /// </summary>
        /// <param name="configAttributeAndTypeInfos"></param>
        /// <exception cref="CanNotConvertToJsonTypeException">当配置类中存在无法转化为 Json 类型时的异常</exception>
        /// <returns></returns>
        string Generate(IEnumerable<AttributeAndTypeInfo> configAttributeAndTypeInfos);
    }
}
