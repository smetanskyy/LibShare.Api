using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces.IRepositories;
using LibShare.Api.Helpers;
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
            item.Image = null;
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
                return GetAll();

            return FilterByMultiCategory(new[] { chosenCategoryId });
        }

        public IEnumerable<Book> FilterByMultiCategory(string[] chosenCategories)
        {
            if (chosenCategories == null || !chosenCategories.Any())
                return GetAll();

            chosenCategories = _categoryRepo.GetAllSubCategoriesIdFromFilter(chosenCategories);

            var books = GetBooksWithoutDeleteItems();

            if (BookParameters.OnlyEbooks == true)
                books = books.Where(b => b.IsEbook == true).AsQueryable();
            else if (BookParameters.OnlyRealBooks == true)
                books = books.Where(b => b.IsEbook == false).AsQueryable();

            return books
                .Where(c => chosenCategories.Contains(c.CategoryId)).AsQueryable();
        }

        public async Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                var books = GetBooksWithoutDeleteItems();
                return await books.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Book> GetAll()
        {
            var books = GetBooksWithoutDeleteItems();

            if (BookParameters.OnlyEbooks == true)
                return books.Where(b => b.IsEbook == true).AsQueryable();
            else if (BookParameters.OnlyRealBooks == true)
                return books.Where(b => b.IsEbook == false).AsQueryable();
            else
                return books;
        }

        public IEnumerable<Book> GetBooksByUserId(string userId)
        {
            var books = GetBooksWithoutDeleteItems();
            books = books.Where(b => b.UserId == userId).AsQueryable();

            if (BookParameters.OnlyEbooks == true)
                return books.Where(b => b.IsEbook == true).AsQueryable();
            else if (BookParameters.OnlyRealBooks == true)
                return books.Where(b => b.IsEbook == false).AsQueryable();
            else
                return books;
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
            var books = GetBooksWithoutDeleteItems();

            if (BookParameters.OnlyEbooks == true)
                books = books.Where(b => b.IsEbook == true).AsQueryable();
            else if (BookParameters.OnlyRealBooks == true)
                books = books.Where(b => b.IsEbook == false).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                       || b.Author.Contains(searchString)
                                       || b.Publisher.Contains(searchString)
                                       || b.Year.Contains(searchString)
                                       || b.Description.Contains(searchString)).AsQueryable();
            }

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
                    books = books.OrderBy(b => b.Author).ThenBy(b => b.Title);
                    break;
                case SortOrder.Publisher:
                    books = books.OrderBy(b => b.Publisher).ThenBy(b => b.Title);
                    break;
                case SortOrder.Year:
                    books = books.OrderByDescending(b => b.Year).ThenBy(b => b.Title);
                    break;
                case SortOrder.Language:
                    books = books.OrderBy(b => b.Language).ThenBy(b => b.Title);
                    break;
                case SortOrder.Description:
                    books = books.OrderBy(b => b.Description);
                    break;
                case SortOrder.Category:
                    books = books.OrderBy(b => b.CategoryId).ThenBy(b => b.Title);
                    break;
                case SortOrder.User:
                    books = books.OrderBy(b => b.UserId).ThenBy(b => b.Title);
                    break;
                case SortOrder.DateCreate:
                    books = books.OrderByDescending(b => b.DateCreate).ThenBy(b => b.Title);
                    break;
                case SortOrder.LookedRate:
                    books = books.OrderByDescending(b => b.LookedRate).ThenBy(b => b.Title);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
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

        private IQueryable<Book> GetBooksWithoutDeleteItems()
        {
            return _context.Books.Where(b => b.IsDeleted == false).AsQueryable();
        }

        public async Task<bool> LookedRate(string bookId)
        {
            try
            {
                var book = await GetByIdAsync(bookId);
                book.LookedRate++;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SetFileName(string bookId, string filename)
        {
            try
            {
                var book = await GetByIdAsync(bookId);
                book.File = filename;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Book> GetByIdWithUserAsync(string bookId)
        {
            if (bookId == null)
                return null;
            
            return await _context.Books.Include(b => b.DbUser).Where(b => b.Id == bookId).FirstOrDefaultAsync();
        }
    }
}
