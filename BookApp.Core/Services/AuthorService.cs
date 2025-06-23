using BookApp.Core.Interfaces;
using BookApp.Core.Models;
using System.Text.Json;

namespace BookApp.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly ILoggerService _loggerService;


        public AuthorService(IAuthorRepository repository, ILoggerService loggerService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<Author> AddAsync(Author author)
        {
            EnsureAuthorNotNull(author);

            try
            {
                return await _repository.AddAsync(author);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error adding author: {ex.Message}", ex);
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
                _loggerService.LogError($"Error deleting author with ID {id}: {ex.Message}", ex);
                throw;
            }
        }


        public async Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    var error = new ArgumentException("Author name cannot be null or empty", nameof(name));
                    _loggerService.LogError("Author name cannot be null or empty", error);
                    throw error;
                }

                var authors = await _repository.GetAsync();
                return authors.Where(a =>
                    !string.IsNullOrWhiteSpace(a.Name) &&
                    a.Name.IndexOf(name.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error retrieving authors: {ex.Message}", ex);
                throw;
            }
        }


        public Task<Author> UpdateAsync(Author author)
        {
            EnsureAuthorNotNull(author);

            try
            {
                return _repository.UpdateAsync(author);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error updating author: {ex.Message}", ex);
                throw;
            }
        }


        private void EnsureAuthorNotNull(Author author)
        {
            if (author == null)
            {
                var error = new ArgumentNullException(nameof(author), "Author cannot be null");
                _loggerService.LogError("Null author detected", error);
                throw error;
            }
        }
    }
}
