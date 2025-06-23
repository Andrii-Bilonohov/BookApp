using System.Text.Json.Serialization;

namespace BookApp.Api.Models
{
    public class WrapperAuthor
    {
        [JsonPropertyName("numFound")]
        public int NumFound { get; set; }
        [JsonPropertyName("start")]
        public int Start { get; set; }
        [JsonPropertyName("numFoundExact")]
        public bool NumFoundExact { get; set; }
        [JsonPropertyName("num_found")]
        public int NumFoundLegacy { get; set; }
        [JsonPropertyName("documentation_url")]
        public string? DocumentationUrl { get; set; }
        [JsonPropertyName("q")]
        public string? Query { get; set; }
        [JsonPropertyName("offset")]
        public int? Offset { get; set; }
        [JsonPropertyName("docs")]
        public List<AuthorForFindBooksApi> Docs { get; set; } = new List<AuthorForFindBooksApi>();

        public override string ToString()
        {
            var result = $"NumFound: {NumFound}\n" +
                         $"Start: {Start}\n" +
                         $"NumFoundExact: {NumFoundExact}\n" +
                         $"NumFoundLegacy: {NumFoundLegacy}\n" +
                         $"DocumentationUrl: {DocumentationUrl}\n" +
                         $"Query: {Query}\n" +
                         $"Offset: {Offset}\n";

            foreach (var doc in Docs)
            {
                result += "\n-----------------------------\n";
                result += doc?.ToString();
            }

            return result;
        }
    }
}
