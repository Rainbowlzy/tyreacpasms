using Newtonsoft.Json;

namespace TENtities
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
        }
    }
}