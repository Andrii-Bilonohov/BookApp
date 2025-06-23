using BookApp.Core.Interfaces;
using BookApp.Core.Models;
using BookApp.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Storage.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly BookContext _context;


        public AuthorBookRepository(BookContext context)
        {
            _context = context;
        }


        public async Task AddAsync(AuthorBook authorBook)
        {
            if (authorBook == null)
            {
                throw new ArgumentNullException(nameof(authorBook));
            }

            await _context.AuthorBooks.AddAsync(authorBook);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var authorBook = await _context.AuthorBooks.FindAsync(id);
            if (authorBook == null)
            {
                throw new InvalidOperationException($"AuthorBook with id {id} not found.");
            }

            _context.AuthorBooks.Remove(authorBook);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<AuthorBook>> GetAsync()
        {
            return await _context.AuthorBooks
                .Include(ab => ab.Author)
                .Include(ab => ab.Book)
                .ToListAsync();
        }


        public async Task UpdateAsync(AuthorBook authorBook)
        {
            if (authorBook == null)
            {
                throw new ArgumentNullException(nameof(authorBook));
            }

            var existing = await _context.AuthorBooks.FindAsync(authorBook.Id);
            if (existing == null)
            {
                throw new InvalidOperationException($"AuthorBook with id {authorBook.Id} not found.");
            }

            existing.AuthorId = authorBook.AuthorId;
            existing.BookId = authorBook.BookId;

            _context.AuthorBooks.Update(existing);
            await _context.SaveChangesAsync();
        }
    }
}
