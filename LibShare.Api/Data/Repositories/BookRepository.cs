using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book item)
        {
            if (item == null)
                return null;
            _context.Books.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Book> DeleteAsync(string id, string deletionReason)
        {
            var book = await _context.Books.FindAsync(id);
            try
            {
                book.IsDeleted = true;
                await _context.SaveChangesAsync();
                return book;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                return await _context.Books.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(Book item)
        {
            if (item == null)
                return false;
            try
            {
                _context.Books.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
