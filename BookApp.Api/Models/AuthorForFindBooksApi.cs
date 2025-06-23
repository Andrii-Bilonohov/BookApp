using System.Text.Json.Serialization;

namespace BookApp.Api.Models
{
    public class AuthorForFindBooksApi
    {
        [JsonPropertyName("author_key")]
        public List<string>? AuthorKey { get; set; }
        [JsonPropertyName("author_name")]
        public List<string> AuthorName { get; set; }
        [JsonPropertyName("cover_edition_key")]
        public string? CoverEditionKey { get; set; }
        [JsonPropertyName("cover_i")]
        public int? CoverId { get; set; }
        [JsonPropertyName("ebook_access")]
        public string? EbookAccess { get; set; }
        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }
        [JsonPropertyName("first_publish_year")]
        public int FirstPublishYear { get; set; }
        [JsonPropertyName("has_fulltext")]
        public bool? HasFulltext { get; set; }
        [JsonPropertyName("key")]
        public string? Key { get; set; }
        [JsonPropertyName("public_scan_b")]
        public bool? PublicScan { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        public override string ToString()
        {
           return  $"Author Keys: {string.Join(", ", AuthorKey ?? new List<string>())}\n" +
                   $"Author Names: {string.Join(", ", AuthorName ?? new List<string>())}\n" +
                   $"Cover Edition Key: {CoverEditionKey}\n" +
                   $"Cover ID: {CoverId}\n" +
                   $"Ebook Access: {EbookAccess}\n" +
                   $"Edition Count: {EditionCount}\n" +
                   $"First Publish Year: {FirstPublishYear}\n" +
                   $"Has Fulltext: {HasFulltext}\n" +
                   $"Key: {Key}\n" +
                   $"Public Scan: {PublicScan}\n" +
                   $"Title: {Title}\n";
        }
    }
}
