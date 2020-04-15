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
        public string Generate(IEnumerable<IConfigRegistion> registions)
        {
            JObject jo = new JObject();
            jo[Constant.PROPERTY_SCHEMA] = Constant.SCHEMA_DEFAULT;
            jo[Constant.PROPERTY_TYPE] = Constant.TYPE_OBJECT;
            JObject properties = new JObject();
            jo[Constant.PROPERTY_PROPERTIES] = properties;

            foreach (var registion in registions)
            {

                string section = registion.Section;

                JsonObjectBuilder builder = new JsonObjectBuilder()
                {
                    Description = registion.Type.GetDescription(),
                    Type = registion.Type
                };
                properties[section] = builder.Build();

            }
            return jo.ToString();
        }
    }
}
