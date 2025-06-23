using BookApp.Core.Interfaces;
using BookApp.Core.Models;
using BookApp.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Storage.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookContext _context;


        public AuthorRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Author> AddAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var author = _context.Authors.Find(id);
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }


        public async Task<IEnumerable<Author>> GetAsync()
        {
            return await _context.Authors.ToListAsync();
        }


        public async Task<Author> UpdateAsync(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Update(author);
            return await _context.SaveChangesAsync().ContinueWith(t => author);
        }
    }
}
