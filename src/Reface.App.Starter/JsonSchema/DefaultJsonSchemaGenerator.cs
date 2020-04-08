using Newtonsoft.Json.Linq;
using Reface.AppStarter.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Reface.AppStarter.JsonSchema
{
    /// <summary>
    /// <see cref="IJsonSchemaGenerator"/> 的默认实现类，
    /// 使用 Newtonsoft.Json 中的 <see cref="JObject"/> 作为媒介生成 Json，
    /// 生成的过程中，对于不可写属性将不生成 Schema
    /// </summary>
    [Component]
    public class DefaultJsonSchemaGenerator : IJsonSchemaGenerator
    {
        public string Generate(IEnumerable<AttributeAndTypeInfo> configAttributeAndTypeInfos)
        {
            JObject jo = new JObject();
            jo[Constant.PROPERTY_SCHEMA] = Constant.SCHEMA_DEFAULT;
            jo[Constant.PROPERTY_TYPE] = Constant.TYPE_OBJECT;
            JObject properties = new JObject();
            jo[Constant.PROPERTY_PROPERTIES] = properties;

            foreach (var info in configAttributeAndTypeInfos)
            {
                if (IsNotConfigAttribute(info)) continue;
                ConfigAttribute configAttribute = (ConfigAttribute)info.Attribute;

                string section = configAttribute.Section;

                JsonObjectBuilder builder = new JsonObjectBuilder()
                {
                    Description = info.Type.GetDescription(),
                    Type = info.Type
                };
                properties[section] = builder.Build();

            }
            return jo.ToString();
        }

        private bool IsConfigAttribute(AttributeAndTypeInfo info)
        {
            return info.Attribute is ConfigAttribute;
        }

        private bool IsNotConfigAttribute(AttributeAndTypeInfo info)
        {
            return !IsConfigAttribute(info);
        }

    }
}
