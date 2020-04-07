using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.AppStarter.JsonSchema
{
    public class JsonPropertiesBuilder
    {
        public IEnumerable<PropertyInfo> PropertyInfos { get; set; }

        public JObject Build()
        {
            JObject jProps = new JObject();
            foreach (var prop in PropertyInfos)
            {
                if (!prop.CanWrite) continue;

                JsonPropertyBuilder builder = new JsonPropertyBuilder()
                {
                    Description = prop.GetDescription(),
                    Type = prop.PropertyType
                };

                jProps[prop.Name] = builder.Build();
            }
            return jProps;
        }
    }
}
