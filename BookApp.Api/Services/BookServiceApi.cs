using BookApp.Api.Interfaces;
using BookApp.Api.Models;
using BookApp.Core.Interfaces;
using System.Text.Json;

namespace BookApp.Api.Services
{
    public class BookServiceApi : IBookServiceApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService _loggerService;

        public BookServiceApi(HttpClient httpClient, ILoggerService loggerService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://openlibrary.org/search.json");
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<WrapperBook> GetByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                var error = new ArgumentException("Title cannot be null or empty", nameof(title));
                _loggerService.LogError("Title cannot be null or empty", error);
                throw error;
            }

            var response = await _httpClient.GetAsync($"?title={title}");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError($"Error fetching data for title '{title}': {response.ReasonPhrase}", error);
                throw error;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var bookWrapper = JsonSerializer.Deserialize<WrapperBook>(content);
                if (bookWrapper == null)
                {
                    var error = new ArgumentNullException("Deserialized book is null");
                    _loggerService.LogError($"Deserialized book is null for title '{title}'", error);
                    throw error;
                }

                return bookWrapper;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response for title '{title}': {ex.Message}", ex);
                throw new InvalidOperationException("Failed to deserialize the response");
            }
        }


        public async Task<WrapperBook> GetByAuthorAsync(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                var error = new ArgumentException("Author name cannot be null or empty", nameof(authorName));
                _loggerService.LogError("Author name cannot be null or empty", error);
                throw error;
            }

            var response = await _httpClient.GetAsync($"?author={authorName}&sort=new");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError($"Error fetching data for author '{authorName}': {response.ReasonPhrase}", error);
                throw error;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var bookWrapper = JsonSerializer.Deserialize<WrapperBook>(content);
                if (bookWrapper == null)
                {
                    var error = new ArgumentNullException("Deserialized book is null");
                    _loggerService.LogError($"Deserialized book is null for author '{authorName}'", error);
                    throw error;
                }

                bookWrapper.Docs = bookWrapper.Docs
                    .Where(b => b.AuthorName != null && b.AuthorName.Any(name => name.Contains(authorName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                return bookWrapper;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response for author '{authorName}': {ex.Message}", ex);
                throw new InvalidOperationException("Failed to deserialize the response");
            }
        }
    }
}
