using System.Text.Json.Serialization;

namespace BookApp.Api.Models
{
    public class WrapperBook
    {
        [JsonPropertyName("numFound")]
        public int TotalBooks { get; set; }
        [JsonPropertyName("start")]
        public int StartIndex { get; set; }
        [JsonPropertyName("numFoundExact")]
        public bool IsExactCount { get; set; }
        [JsonPropertyName("num_found")]
        public int NumFound { get; set; }
        [JsonPropertyName("documentation_url")]
        public string? DocumentationUrl { get; set; }
        [JsonPropertyName("q")]
        public string? Query { get; set; }
        [JsonPropertyName("offset")]
        public int? Offset { get; set; }
        [JsonPropertyName("docs")]
        public ICollection<BookApi> Docs { get; set; } = new List<BookApi>();

        public override string ToString()
        {
            var result = string.Empty;

            foreach (var doc in Docs)
            {
                result += "\n-----------------------------\n";
                result += doc?.ToString();
            }

            return result;
        }
    }
}