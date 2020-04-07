using Newtonsoft.Json.Linq;
using Reface.AppStarter.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Reface.AppStarter.JsonSchema
{
    [Component]
    public class DefaultJsonSchemaGenerator : IJsonSchemaGenerator
    {
        public string Generate(IEnumerable<AttributeAndTypeInfo> configAttributeAndTypeInfos)
        {
            JObject jo = new JObject();
            jo["$schema"] = "http://json-schema.org/schema#";
            jo["type"] = "object";
            JObject properties = new JObject();
            jo["properties"] = properties;

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
