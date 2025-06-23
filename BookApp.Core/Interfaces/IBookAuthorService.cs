using BookApp.Core.Models;

namespace BookApp.Core.Interfaces
{
    public interface IBookAuthorService
    {
        Task<IEnumerable<AuthorBook>> GetBooksByAuthorNameAsync(string name);
    }
}
