using BookApp.Core.Interfaces;
using BookApp.Core.Models;
using System.Linq;

namespace BookApp.Core.Services
{
    public class BookService : IBookService, IBookAuthorService
    {
        private readonly ILoggerService _loggerService;
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository, ILoggerService loggerService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<Book> AddAsync(Book book)
        {
            EnsureBookNotNull(book);

            try
            {
                return await _repository.AddAsync(book);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error adding book: {ex.Message}", ex);
                throw;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                var error = new ArgumentException("ID must be greater than zero", nameof(id));
                _loggerService.LogError("Invalid ID provided for deletion", error);
                throw error;
            }

            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error deleting book with ID {id}: {ex.Message}", ex);
                throw;
            }
        }


        public async Task<IEnumerable<Book>> GetByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                var error = new ArgumentException("Title cannot be null or empty", nameof(title));
                _loggerService.LogError("Title cannot be null or empty", error);
                throw error;
            }

            try
            {
                var books = await _repository.GetAsync();

                if (books == null || !books.Any())
                {
                    _loggerService.LogWarning("No books found in the repository.");
                    return Enumerable.Empty<Book>();
                }

                var filteredBooks = books.Where(b =>
                    !string.IsNullOrWhiteSpace(b.Title) &&
                    b.Title.IndexOf(title.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                return filteredBooks;
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching books by title '{title}': {ex.Message}", ex);
                throw;
            }
        }



        public async Task<IEnumerable<AuthorBook>> GetBooksByAuthorNameAsync(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                var error = new ArgumentException("Author name cannot be null or empty", nameof(authorName));
                _loggerService.LogError("Author name cannot be null or empty", error);
                throw error;
            }

            try
            {
                var books = await _repository.GetAsync();
                if (books == null || !books.Any())
                {
                    _loggerService.LogWarning("No books found in the repository.");
                    return Enumerable.Empty<AuthorBook>();
                }

                var filteredAuthorBooks = new List<AuthorBook>();

                foreach (var book in books)
                {
                    if (book.BookAuthors == null) continue;

                    var matchingAuthors = book.BookAuthors
                        .Where(ab => ab.Author != null &&
                                     !string.IsNullOrEmpty(ab.Author.Name) &&
                                     ab.Author.Name.IndexOf(authorName.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

                    foreach (var authorBook in matchingAuthors)
                    {
                        filteredAuthorBooks.Add(authorBook);
                    }
                }

                return filteredAuthorBooks;
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching books by author '{authorName}': {ex.Message}", ex);
                throw;
            }
        }




        public async Task<Book> UpdateAsync(Book book)
        {
            EnsureBookNotNull(book);

            try
            {
                return await _repository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error updating book: {ex.Message}", ex);
                throw;
            }
        }


        private void EnsureBookNotNull(Book book)
        {
            if (book == null)
            {
                var error = new ArgumentNullException(nameof(book), "Book cannot be null");
                _loggerService.LogError("Null author detected", error);
                throw error;
            }
        }
    }
}
