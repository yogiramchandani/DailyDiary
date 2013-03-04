using System;
using System.Net.Http;
using Xunit;

namespace DailyDiaryApi.AcceptanceTests
{
    public class DailyDiaryJsonTests
    {
        [Fact]
        public void Get_WithEmptyCall_ResponseReturnCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {   
                var response = client.GetAsync("").Result;
                Assert.True(response.IsSuccessStatusCode, "Actual: " + response.StatusCode);
            }
        }
        
        [Fact]
        public void Post_WithJson_ResponseReturnCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var json = new
                    {
                        title = "test",
                        date = DateTimeOffset.Now,
                        content = ""
                    };
                var expected = json.ToJObject();
                client.PostAsJsonAsync("", json).Wait();

                var response = client.GetAsync("").Result;

                dynamic actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.entries);
            }
        }
    }
}
