using Newtonsoft.Json.Linq;
using Reface.AppStarter.Errors;
using System;

namespace Reface.AppStarter.JsonSchema
{
    public class JsonItemsBuilder
    {
        public Type ItemType { get; set; }
        public JObject Build()
        {
            JObject jItems = new JObject();
            if (JsonTypeHelper.IsString(this.ItemType))
                jItems[Constant.PROPERTY_TYPE] = Constant.TYPE_STRING;
            else if (JsonTypeHelper.IsNumber(this.ItemType))
                jItems[Constant.PROPERTY_TYPE] = Constant.TYPE_NUMBER;
            else if (JsonTypeHelper.IsBoolean(this.ItemType))
                jItems[Constant.PROPERTY_TYPE] = Constant.TYPE_BOOLEAN;
            else if (JsonTypeHelper.IsArray(this.ItemType))
            {
                jItems[Constant.PROPERTY_TYPE] = Constant.TYPE_ARRAY;

                JsonItemsBuilder builder = new JsonItemsBuilder()
                {
                    ItemType = JsonTypeHelper.GetArrayItemType(this.ItemType)
                };
                jItems[Constant.PROPERTY_ITEMS] = builder.Build();
            }
            else if (JsonTypeHelper.IsObject(this.ItemType))
            {
                JsonObjectBuilder builder = new JsonObjectBuilder()
                {
                    Description = this.ItemType.GetDescription(),
                    Type = this.ItemType
                };
                return builder.Build();
            }
            else throw new CanNotConvertToJsonTypeException(this.ItemType);

            return jItems;
        }
    }
}
