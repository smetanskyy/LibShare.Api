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
    public class CategoryRepository : ICrudRepository<Category, string>
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category item)
        {
            if (item == null)
                return null;
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            try
            {
                return await _context.Categories.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
