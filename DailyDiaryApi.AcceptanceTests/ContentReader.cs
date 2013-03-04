using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DailyDiaryApi.AcceptanceTests
{
    public static class ContentReader
    {
        public static Task<dynamic> ReadAsJsonAsync(this HttpContent content)
        {
            if(content == null)
                throw new ArgumentNullException("content");
            return content.ReadAsStringAsync().ContinueWith(x => JsonConvert.DeserializeObject(x.Result));
        }

        public static dynamic ToJObject(this object value)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value));
        }
    }
}
