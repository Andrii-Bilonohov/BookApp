using BookApp.Core.Interfaces;
using BookApp.Core.Models;

namespace BookApp.Core.Services
{
    public class AuthorBookService : IAuthorBookService
    {
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly ILoggerService _loggerService;


        public AuthorBookService(IAuthorBookRepository authorBookRepository, ILoggerService loggerService)
        {
            _authorBookRepository = authorBookRepository ?? throw new ArgumentNullException(nameof(authorBookRepository));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<IEnumerable<AuthorBook>> GetBooksByAuthorNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Author name cannot be null or empty.", nameof(name));
            }

            try
            {
                var authorBooks = await _authorBookRepository.GetAsync();
                
                if (authorBooks == null || !authorBooks.Any())
                {
                    _loggerService.LogWarning($"No books found for author '{name}'");
                    return Enumerable.Empty<AuthorBook>();
                }

                return authorBooks
                    .Where(ab => ab.Author != null && ab.Author.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error retrieving books for author '{name}': {ex.Message}", ex);
                throw;
            }
        }
    }
}
