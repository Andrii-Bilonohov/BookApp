using System.Text.Json.Serialization;

namespace BookApp.Api.Models
{
    public class BookApi
    {
        [JsonPropertyName("author_key")]
        public List<string>? AuthorKey { get; set; }
        [JsonPropertyName("author_name")]
        public List<string>? AuthorName { get; set; }
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
        [JsonPropertyName("ia")]
        public List<string>? Ia { get; set; }
        [JsonPropertyName("ia_collection_s")]
        public string? IaCollection { get; set; }
        [JsonPropertyName("key")]
        public string? Key { get; set; }
        [JsonPropertyName("language")]
        public List<string>? Language { get; set; }
        [JsonPropertyName("lending_edition_s")]
        public string? LendingEdition { get; set; }
        [JsonPropertyName("lending_identifier_s")]
        public string? LendingIdentifier { get; set; }
        [JsonPropertyName("public_scan_b")]
        public bool? PublicScan { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }


        public override string ToString()
        {
            return $"Title: {Title}\n" +
                   $"Authors: {string.Join(", ", AuthorName ?? new List<string>())}\n" +
                   $"First Published: {FirstPublishYear}\n" +
                   $"Languages: {string.Join(", ", Language ?? new List<string>())}\n" +
                   $"Edition Count: {EditionCount}\n" +
                   $"Cover ID: {CoverId}\n" +
                   $"Ebook Access: {EbookAccess}\n" +
                   $"IA Collection: {IaCollection}\n" +
                   $"Has Full Text: {HasFulltext}\n" +
                   $"Public Scan: {PublicScan}\n" +
                   $"Lending Edition: {LendingEdition}\n" +
                   $"Lending Identifier: {LendingIdentifier}\n" +
                   $"Key: {Key}\n" +
                   $"IA: {string.Join(", ", Ia ?? new List<string>())}\n" +
                   $"Author Keys: {string.Join(", ", AuthorKey ?? new List<string>())}\n";
        }
    }
}
