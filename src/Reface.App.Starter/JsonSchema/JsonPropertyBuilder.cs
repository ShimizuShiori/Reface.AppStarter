using Newtonsoft.Json.Linq;
using Reface.AppStarter.Errors;
using System;

namespace Reface.AppStarter.JsonSchema
{
    public class JsonPropertyBuilder
    {
        public Type Type { get; set; }
        public string Description { get; set; }

        public JObject Build()
        {
            JObject jProp = new JObject();

            if (!string.IsNullOrEmpty(this.Description))
                jProp[Constant.PROPERTY_DESCRIPTION] = Description;

            if (JsonTypeHelper.IsString(this.Type))
                jProp[Constant.PROPERTY_TYPE] = Constant.TYPE_STRING;
            else if (JsonTypeHelper.IsNumber(this.Type))
                jProp[Constant.PROPERTY_TYPE] = Constant.TYPE_NUMBER;
            else if (JsonTypeHelper.IsBoolean(this.Type))
                jProp[Constant.PROPERTY_TYPE] = Constant.TYPE_BOOLEAN;
            else if (JsonTypeHelper.IsArray(this.Type))
            {
                jProp[Constant.PROPERTY_TYPE] = Constant.TYPE_ARRAY;
                JsonItemsBuilder builder = new JsonItemsBuilder()
                {
                    ItemType = JsonTypeHelper.GetArrayItemType(this.Type)
                };
                jProp[Constant.PROPERTY_ITEMS] = builder.Build();
            }
            else if (JsonTypeHelper.IsObject(this.Type))
            {
                JsonObjectBuilder builder = new JsonObjectBuilder()
                {
                    Type = this.Type,
                    Description = this.Description
                };
                return builder.Build();

            }
            else
                throw new CanNotConvertToJsonTypeException(this.Type);

            return jProp;
        }

    }
}
