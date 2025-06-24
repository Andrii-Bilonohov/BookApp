using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IAuthorBookService
    {
        Task<IEnumerable<AuthorBook>> GetBooksByAuthorNameAsync(string name);
        Task<IEnumerable<AuthorBook>> GetAuthorsByAuthorNameAsync(string name);
    }
}
