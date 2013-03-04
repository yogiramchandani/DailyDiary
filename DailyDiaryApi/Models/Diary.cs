namespace DailyDiaryApi.Models
{
    public class Diary
    {
        public string Name { get; set; }
        public DiaryEntry[] Entries { get; set; }
    }
}