using Newtonsoft.Json.Linq;
using System;

namespace Reface.AppStarter.JsonSchema
{
    public class JsonObjectBuilder
    {
        public Type Type { get; set; }

        public string Description { get; set; }

        public JObject Build()
        {
            JObject jObj = new JObject();

            jObj[Constant.PROPERTY_TYPE] = Constant.TYPE_OBJECT;

            if (!string.IsNullOrEmpty(this.Description))
                jObj[Constant.PROPERTY_DESCRIPTION] = this.Description;

            JsonPropertiesBuilder builder = new JsonPropertiesBuilder()
            {
                PropertyInfos = Type.GetProperties()
            };

            jObj[Constant.PROPERTY_PROPERTIES] = builder.Build();

            return jObj;
        }

    }
}
