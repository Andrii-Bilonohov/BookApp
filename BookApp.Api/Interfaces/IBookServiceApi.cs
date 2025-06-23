using BookApp.Api.Models;

namespace BookApp.Api.Interfaces
{
    public interface IBookServiceApi
    {
        Task<WrapperBook> GetByTitleAsync(string title);
        Task<WrapperBook> GetByAuthorAsync(string authorName);
    }
}
