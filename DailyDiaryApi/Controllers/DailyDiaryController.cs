using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DailyDiaryApi.Controllers
{
    public class DailyDiaryController : ApiController
    {
        private readonly static List<DiaryEntry> entries = new List<DiaryEntry>();
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new Diary{Entries = entries.ToArray()});
        }

        public HttpResponseMessage Post(DiaryEntry entry)
        {
            entries.Add(entry);
            return this.Request.CreateResponse();
        }
    }

    public class DiaryEntry
    {
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Content { get; set; }
    }

    public class Diary
    {
        public DiaryEntry[] Entries { get; set; }
    }
}
