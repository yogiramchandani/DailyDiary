using System;

namespace DailyDiaryApi.Models
{
    public class DiaryEntry
    {
        public string Title { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Content { get; set; }
    }

    public class DiaryEntryWithName : DiaryEntry
    {
        public string DiaryName { get; set; }
    }
}