using BookApp.Api.Interfaces;
using BookApp.Api.Models;
using BookApp.Core.Interfaces;
using System.Text.Json;

namespace BookApp.Api.Services
{
    public class AuthorServiceApi : IAuthorServiceApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService _loggerService;


        public AuthorServiceApi(HttpClient httpClient, ILoggerService loggerService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://openlibrary.org/search.json");
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<WrapperAuthor?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var error = new ArgumentException("Author name cannot be null or empty.", nameof(name));
                _loggerService.LogError("Author name cannot be null or empty.", error);
                throw error;
            }

            var response = await _httpClient.GetAsync($"?author={name}&sort=new");
            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError($"Error fetching data for author '{name}': {response.ReasonPhrase}", error);
                throw error;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var authorWrapper = JsonSerializer.Deserialize<WrapperAuthor>(content);
                if (authorWrapper == null)
                {
                    var error = new ArgumentNullException(nameof(authorWrapper), "Deserialized author is null");
                    _loggerService.LogError($"Deserialized author is null for name '{name}'", error);
                    throw error;
                }

                return authorWrapper;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response for author '{name}': {ex.Message}", ex);
                throw new InvalidOperationException("Failed to deserialize the response");
            }
        }
    }
}
