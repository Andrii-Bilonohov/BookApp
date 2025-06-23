using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}
