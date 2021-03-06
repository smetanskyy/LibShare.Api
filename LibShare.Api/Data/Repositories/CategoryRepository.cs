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
    public class CategoryRepository : ICategoryRepository
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

        public async Task<Category> DeleteAsync(string id, string deletionReason)
        {
            var category = await _context.Categories.FindAsync(id);
            try
            {
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
                return category;
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

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Children).AsEnumerable().Where(x => x.Parent == null).ToList();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        private List<Category> GetChildren(List<Category> categories, string id)
        {
            return categories
                .Where(x => x.ParentId == id)
                .Union(categories.Where(x => x.ParentId == id)
                    .SelectMany(y => GetChildren(categories, y.Id))
                ).ToList();
        }

        public string[] GetAllSubCategoriesIdFromFilter(string[] chosenCategories)
        {
            List<string> categoriesId = new List<string>(chosenCategories);

            foreach (var category in chosenCategories)
            {
                List<string> IDs = GetChildren(_context.Categories.ToList(), category).Select(c => c.Id).ToList();
                categoriesId.AddRange(IDs);
            }

            return categoriesId.OrderBy(c => c).Distinct().ToArray();
        }

        public async Task<bool> UpdateAsync(Category item)
        {
            if (item == null)
                return false;
            try
            {
                item.DateModify = DateTime.Now;
                _context.Categories.Update(item);
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
