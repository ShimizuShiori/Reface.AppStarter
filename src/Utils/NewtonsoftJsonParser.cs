using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Reface.AppStarter.Utils
{
    [Component]
    public class NewtonsoftJsonParser : IJsonParser
    {
        public JArray ToArray(string json)
        {
            return JArray.Parse(json);
        }

        public string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T ToObject<T>(string json)
        {

            return JsonConvert.DeserializeObject<T>(json);
        }

        public JObject ToObject(string json)
        {
            return JObject.Parse(json);
        }
    }
}
