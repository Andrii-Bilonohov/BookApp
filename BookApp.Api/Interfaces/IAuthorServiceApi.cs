using BookApp.Api.Models;

namespace BookApp.Api.Interfaces
{
    public interface IAuthorServiceApi
    {
        Task<WrapperAuthor?> GetByNameAsync(string name);
    }
}
