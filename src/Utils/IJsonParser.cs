using Newtonsoft.Json.Linq;

namespace Reface.AppStarter.Utils
{
    public interface IJsonParser
    {
        T ToObject<T>(string json);
        string ToJson(object value);
        JObject ToObject(string json);
        JArray ToArray(string json);
    }
}
