using BookApp.Core.Interfaces;
using BookApp.Core.Models;
using BookApp.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Storage.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await _context.Books.ToListAsync();
        }


        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }


        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false; 
            }
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
