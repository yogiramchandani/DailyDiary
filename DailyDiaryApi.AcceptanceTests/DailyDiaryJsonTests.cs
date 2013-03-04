using System.Net;
using Simple.Data;
using System;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using Xunit;

namespace DailyDiaryApi.AcceptanceTests
{
    public class DailyDiaryJsonTests
    {
        [Fact]
        [UseDatabase]
        public void Get_WithEmptyCall_ResponseReturnCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {   
                var response = client.GetAsync("").Result;
                Assert.True(response.IsSuccessStatusCode, "Actual: " + response.StatusCode);
            }
        }
        
        [Fact]
        [UseDatabase]
        public void Post_WithInvalidDiaryNameJson_ResponseReturnCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                dynamic entry = new ExpandoObject();
                entry.title = "test";
                entry.time = DateTimeOffset.Now;
                entry.content = "some content";
                entry.DiaryName = "DiaryName";

                var response = client.PostAsJsonAsync("", ((object)entry)).Result;

                Assert.True(response.StatusCode == HttpStatusCode.BadRequest, "Actual: " + response.StatusCode);
            }
        }
        
        [Fact]
        [UseDatabase]
        public void Post_WithValidDiaryNameJson_ResposeReturnsCorrectEntry()
        {
            dynamic entry = new ExpandoObject();
            entry.title = "test";
            entry.time = DateTimeOffset.Now;
            entry.content = "some content";

            var expected = ((object)entry).ToJObject();

            entry.DiaryName = "NewDiary";

            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var db = Database.OpenConnection(connectionString);
            db.Diary.Insert(Name: entry.DiaryName);

            using (var client = HttpClientFactory.Create())
            {
                client.PostAsJsonAsync("", ((object)entry)).Wait();

                var response = client.GetAsync("").Result;
                dynamic actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.entries);
            }
        }

        [Fact]
        [UseDatabase]
        public void Get_WithOneDbEntry_ResposeReturnsCorrectEntry()
        {
            dynamic entry = new ExpandoObject();
            entry.title = "test";
            entry.time = DateTimeOffset.Now;
            entry.content = "some content";

            var expected = ((object)entry).ToJObject();

            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var db = Database.OpenConnection(connectionString);
            var newUserId = db.Diary.Insert(Name: "testuser").Id;
            entry.DiaryId = newUserId;
            db.DiaryEntry.Insert(entry);

            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("").Result;
                dynamic actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.entries);
            }
        }
    }
}
