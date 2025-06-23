using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IAuthorBookRepository
    {
        Task<IEnumerable<AuthorBook>> GetAsync();
        Task AddAsync(AuthorBook authorBook);
        Task UpdateAsync(AuthorBook authorBook);
        Task DeleteAsync(int id);
    }
}
