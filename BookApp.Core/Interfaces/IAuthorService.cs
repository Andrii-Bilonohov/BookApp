using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetByNameAsync(string name);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }
}
