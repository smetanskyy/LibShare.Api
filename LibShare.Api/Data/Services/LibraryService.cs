using AutoMapper;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public LibraryService(ICategoryRepository categoryRepo, IBookRepository bookRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<BookApiModel> CreateBookAsync(BookApiModel book)
        {
            var bookDb = _mapper.Map<Book>(book);
            bookDb.Id = Guid.NewGuid().ToString("D");
            var record = await _bookRepo.CreateAsync(bookDb);
            return _mapper.Map<BookApiModel>(record);
        }

        public async Task<BookApiModel> DeleteBookAsync(string bookId, string deletionReason)
        {
            var record = await _bookRepo.DeleteAsync(bookId, deletionReason);
            return _mapper.Map<BookApiModel>(record);
        }

        public PagedListApiModel<BookApiModel> FilterByCategoryPaginateSort(string categoryIdint, SortOrder sortOrder, int pageSize = 10, int page = 1)
        {
            var booksFromDB = _bookRepo.FilterByCategory(categoryIdint);
            booksFromDB = _bookRepo.Sort(booksFromDB, sortOrder);

            var booksAfterPaginate = _mapper.Map<List<BookApiModel>>(_bookRepo.Paginate(booksFromDB, pageSize, page).ToList());

            PagedListApiModel<BookApiModel> books = new PagedListApiModel<BookApiModel>(
                booksAfterPaginate, booksFromDB.Count(), page, pageSize);

            return books;
        }

        public PagedListApiModel<BookApiModel> FilterByMultiCategoryPaginateSort(string[] categories, SortOrder sortOrder, int pageSize = 10, int page = 1)
        {
            var booksFromDB = _bookRepo.FilterByMultiCategory(categories);
            booksFromDB = _bookRepo.Sort(booksFromDB, sortOrder);

            var booksAfterPaginate = _mapper.Map<List<BookApiModel>>(_bookRepo.Paginate(booksFromDB, pageSize, page).ToList());

            PagedListApiModel<BookApiModel> books = new PagedListApiModel<BookApiModel>(
                booksAfterPaginate, booksFromDB.Count(), page, pageSize);

            return books;
        }

        public async Task<PagedListApiModel<BookApiModel>> GetAllBooksAsync(SortOrder sortOrder, int pageSize = 10, int page = 1)
        {
            var booksFromDB = await _bookRepo.GetAllAsync();
            booksFromDB = _bookRepo.Sort(booksFromDB, sortOrder);

            var booksAfterPaginate = _mapper.Map<List<BookApiModel>>(_bookRepo.Paginate(booksFromDB, pageSize, page).ToList());

            PagedListApiModel<BookApiModel> books = new PagedListApiModel<BookApiModel>(
                booksAfterPaginate, booksFromDB.Count(), page, pageSize);

            return books;
        }

        public async Task<BookApiModel> GetBookByIdAsync(string bookId)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            return _mapper.Map<BookApiModel>(book);
        }

        public IEnumerable<CategoryApiModel> GetCategories()
        {
            var categories = _categoryRepo.GetAll();
            return _mapper.Map<IEnumerable<CategoryApiModel>>(categories);
        }

        public PagedListApiModel<BookApiModel> SearchPaginateSort(string searchString, SortOrder sortOrder, int pageSize = 10, int page = 1)
        {
            var booksFromDB = _bookRepo.Search(searchString);
            booksFromDB = _bookRepo.Sort(booksFromDB, sortOrder);

            var booksAfterPaginate = _mapper.Map<List<BookApiModel>>(_bookRepo.Paginate(booksFromDB, pageSize, page).ToList());

            PagedListApiModel<BookApiModel> books = new PagedListApiModel<BookApiModel>(
                booksAfterPaginate, booksFromDB.Count(), page, pageSize);

            return books;
        }

        public async Task<bool> UpdateBookAsync(BookApiModel book)
        {
            return await _bookRepo.UpdateAsync(_mapper.Map<Book>(book));
        }
    }
}
