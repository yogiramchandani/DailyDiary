using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DailyDiaryApi.Models;
using Simple.Data;

namespace DailyDiaryApi.Controllers
{
    public class DailyDiaryController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var db = Database.OpenConnection(connectionString);

            var entries =
                db.DiaryEntry
                  .All()
                  .ToArray<DiaryEntry>();

            return this.Request.CreateResponse(
                HttpStatusCode.OK, 
                new Diary{Entries = entries});
        }

        public HttpResponseMessage Post(DiaryEntryWithName entry)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dailydiarydb"].ConnectionString;
            var db = Database.OpenConnection(connectionString);

            var diary = db.Diary.Find(db.Diary.Name == entry.DiaryName);

            if (diary == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Diary name specified");
            }

            var entryId = db.DiaryEntry.Insert(
                DiaryId: diary.Id, 
                Title: entry.Title,
                Time: entry.Time,
                Content: entry.Content
                ).Id;

            return this.Request.CreateResponse(HttpStatusCode.OK, (string)entryId.ToString());
        }
    }
}
