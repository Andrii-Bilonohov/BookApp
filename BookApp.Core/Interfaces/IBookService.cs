using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetByTitleAsync(string title);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}
