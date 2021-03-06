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
        private readonly ICategoryRepository _categoryRepo;

        public BookRepository(ApplicationDbContext context, ICategoryRepository categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
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
                book.DateDelete = DateTime.Now;
                book.DeletionReason = deletionReason;
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

        public IEnumerable<Book> FilterByCategory(string chosenCategoryId)
        {
            if (chosenCategoryId == null || string.IsNullOrWhiteSpace(chosenCategoryId))
                return _context.Books.AsQueryable();

            return FilterByMultiCategory(new[] { chosenCategoryId });
        }

        public IEnumerable<Book> FilterByMultiCategory(string[] chosenCategories)
        {
            if (chosenCategories == null || !chosenCategories.Any())
                return _context.Books.AsQueryable();

            chosenCategories = _categoryRepo.GetAllSubCategoriesIdFromFilter(chosenCategories);

            var books = _context.Books
                .Where(b => b.IsDeleted == false)
                .Where(c => chosenCategories.Contains(c.CategoryId)).AsQueryable();

            return books;
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                var books = _context.Books.Where(b => b.IsDeleted == false);
                return await books.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Where(b => b.IsDeleted == false).AsQueryable();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);
        }

        public List<Book> Paginate(IEnumerable<Book> books, int pageSize = 10, int page = 1)
        {
            return books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<Book> Search(string searchString)
        {
            var books = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                       || b.Author.Contains(searchString)
                                       || b.Publisher.Contains(searchString)
                                       || b.Year.Contains(searchString)
                                       || b.Description.Contains(searchString)).AsQueryable();
            }
            if (books == null) new List<Book>();
            return books;
        }

        public IEnumerable<Book> Sort(IEnumerable<Book> books, SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Id:
                    books = books.OrderBy(b => b.Id);
                    break;
                case SortOrder.Title:
                    books = books.OrderBy(b => b.Title);
                    break;
                case SortOrder.Author:
                    books = books.OrderBy(b => b.Author);
                    break;
                case SortOrder.Publisher:
                    books = books.OrderBy(b => b.Publisher);
                    break;
                case SortOrder.Year:
                    books = books.OrderByDescending(b => b.Year);
                    break;
                case SortOrder.Language:
                    books = books.OrderBy(b => b.Language);
                    break;
                case SortOrder.Description:
                    books = books.OrderBy(b => b.Description);
                    break;
                case SortOrder.Category:
                    books = books.OrderBy(b => b.CategoryId);
                    break;
                case SortOrder.User:
                    books = books.OrderBy(b => b.UserId);
                    break;
                case SortOrder.DateCreate:
                    books = books.OrderByDescending(b => b.DateCreate);
                    break;
                default:
                    break;
            }
            return books;
        }

        public async Task<bool> UpdateAsync(Book item)
        {
            if (item == null)
                return false;
            try
            {
                item.DateModify = DateTime.Now;
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
