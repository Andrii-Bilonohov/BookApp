using System.Text.Json.Serialization;

namespace BookApp.Api.Models
{
    public class AuthorApi
    {
        [JsonPropertyName("alternate_names")]
        public List<string>? AlternateNames { get; set; } = new List<string>();
        [JsonPropertyName("birth_year")]
        public string? BirthDate { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set; }
        [JsonPropertyName("death_year")]
        public string? DeathDate { get; set; }
        [JsonPropertyName("key")]
        public string? Key { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("top_subjects")]
        public List<string>? TopSubjects { get; set; } = new List<string>();
        [JsonPropertyName("top_work")]
        public string? TopWork { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("work_count")]
        public int? WorkCount { get; set; }
        [JsonPropertyName("ratings_average")]
        public double? RatingsAverage { get; set; }
        [JsonPropertyName("ratings_sortable")]
        public double? RatingsSortable { get; set; }
        [JsonPropertyName("ratings_count")]
        public int? RatingsCount { get; set; }
        [JsonPropertyName("ratings_count1")]
        public int? RatingsCount1 { get; set; }
        [JsonPropertyName("ratings_count2")]
        public int? RatingsCount2 { get; set; }
        [JsonPropertyName("ratings_count3")]
        public int? RatingsCount3 { get; set; }
        [JsonPropertyName("ratings_count4")]
        public int? RatingsCount4 { get; set; }
        [JsonPropertyName("ratings_count5")]
        public int? RatingsCount5 { get; set; }
        [JsonPropertyName("want_to_read_count")]
        public int? WantReadsCount { get; set; }
        [JsonPropertyName("already_read_count")]
        public int? AlreadyReadCount { get; set; }
        [JsonPropertyName("currently_reading_count")]
        public int? CurrentlyReadingCount { get; set; }
        [JsonPropertyName("readinglog_count")]
        public int? ReadinglogCount { get; set; }
        [JsonPropertyName("_version_")]
        public double? Version { get; set; }


        public override string ToString()
        {
            return $"Alternate Names: {string.Join(", ", AlternateNames ?? new List<string>())}\n" +
                   $"Birth Date: {BirthDate}\n" +
                   $"Date: {Date}\n" +
                   $"Death Date: {DeathDate}\n" +
                   $"Key: {Key}\n" +
                   $"Name: {Name}\n" +
                   $"Top Subjects: {string.Join(", ", TopSubjects ?? new List<string>())}\n" +
                   $"Top Work: {TopWork}\n" +
                   $"Type: {Type}\n" +
                   $"Work Count: {WorkCount}\n" +
                   $"Ratings Average: {RatingsAverage}\n" +
                   $"Ratings Sortable: {RatingsSortable}\n" +
                   $"Ratings Count: {RatingsCount}\n" +
                   $"Ratings Count 1: {RatingsCount1}\n" +
                   $"Ratings Count 2: {RatingsCount2}\n" +
                   $"Ratings Count 3: {RatingsCount3}\n" +
                   $"Ratings Count 4: {RatingsCount4}\n" +
                   $"Ratings Count 5: {RatingsCount5}\n" +
                   $"Want to Read Count: {WantReadsCount}\n" +
                   $"Already Read Count: {AlreadyReadCount}\n" +
                   $"Currently Reading Count: {CurrentlyReadingCount}\n" +
                   $"Reading Log Count: {ReadinglogCount}\n" +
                   $"Version: {Version}\n";
        }
    }
}
