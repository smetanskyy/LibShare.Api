using AutoMapper;
using LibShare.Api.Data.ApiModels.RequestApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
using LibShare.Api.Helpers;
using LibShare.Api.Infrastructure.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;
        private readonly ResourceManager _resourceManager;
        private readonly IEmailService _emailService;
        private readonly UserManager<DbUser> _userManager;

        public LibraryService(
            ICategoryRepository categoryRepo,
            IBookRepository bookRepo,
            IMapper mapper,
            ResourceManager resourceManager,
            IEmailService emailService,
            UserManager<DbUser> userManager)
        {
            _categoryRepo = categoryRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<BookApiModel> CreateBookAsync(BookApiModel book)
        {
            var bookDb = _mapper.Map<Book>(book);
            bookDb.Id = Guid.NewGuid().ToString("D");
            var record = await _bookRepo.CreateAsync(bookDb);
            return _mapper.Map<BookApiModel>(record);
        }

        public async Task<MessageApiModel> DeleteBookAsync(string bookId)
        {
            await GetBookByIdAsync(bookId);
            await _bookRepo.DeleteAsync(bookId, _resourceManager.GetString("ClientDecision"));
            return new MessageApiModel { Message = _resourceManager.GetString("BookDeleted") };
        }

        private PagedListApiModel<BookApiModel> SortPaginate(IEnumerable<Book> list)
        {
            list = _bookRepo.Sort(list, BookParameters.SortOrder);

            var booksAfterPaginate = _mapper.Map<List<BookApiModel>>(
                _bookRepo.Paginate(list, BookParameters.PageSize, BookParameters.PageNumber).ToList());

            PagedListApiModel<BookApiModel> books = new PagedListApiModel<BookApiModel>(
                booksAfterPaginate, list.Count(), BookParameters.PageSize, BookParameters.PageNumber);

            return books;
        }

        public PagedListApiModel<BookApiModel> FilterByCategorySortPaginate(string categoryId)
        {
            var booksFromDB = _bookRepo.FilterByCategory(categoryId);

            return SortPaginate(booksFromDB);
        }

        public PagedListApiModel<BookApiModel> FilterByMultiCategorySortPaginate(string[] categories)
        {
            var booksFromDB = _bookRepo.FilterByMultiCategory(categories);

            return SortPaginate(booksFromDB);
        }

        public PagedListApiModel<BookApiModel> GetAllBooksSortPaginate()
        {
            var booksFromDB = _bookRepo.GetAll();
            return SortPaginate(booksFromDB);
        }

        public async Task<BookApiModel> GetBookByIdAsync(string bookId)
        {
            var result = await _bookRepo.LookedRate(bookId);
            if (result == false)
                throw new NotFoundException(_resourceManager.GetString("BookNotFound"));
            var book = await _bookRepo.GetByIdAsync(bookId);
            return _mapper.Map<BookApiModel>(book);
        }

        public PagedListApiModel<BookApiModel> GetBooksByUserIdSortPaginate(string userId)
        {
            var booksFromDB = _bookRepo.GetBooksByUserId(userId);
            return SortPaginate(booksFromDB);
        }

        public IEnumerable<CategoryApiModel> GetCategories()
        {
            var categories = _categoryRepo.GetAll();
            return _mapper.Map<IEnumerable<CategoryApiModel>>(categories);
        }

        public PagedListApiModel<BookApiModel> SearchSortPaginate(string searchString)
        {
            var booksFromDB = _bookRepo.Search(searchString);
            return SortPaginate(booksFromDB);
        }

        public async Task<BookApiModel> UpdateBookAsync(BookApiModel book)
        {
            await GetBookByIdAsync(book.Id);
            await _bookRepo.UpdateAsync(_mapper.Map<Book>(book));
            return await GetBookByIdAsync(book.Id);
        }

        public async Task<CategoryApiModel> GetCategoryByIdAsync(string categoryId)
        {
            var category = await _categoryRepo.GetByIdAsync(categoryId);
            if (category == null)
                throw new NotFoundException(_resourceManager.GetString("BookNotFound"));
            return _mapper.Map<CategoryApiModel>(category);
        }

        public async Task<MessageApiModel> SendMessageToOwner(string userId, CallOwnerApiModel model)
        {
            var book = await _bookRepo.GetByIdWithUserAsync(model.BookId);
            var user = await _userManager.Users.Include(u => u.UserProfile).Where(u => u.Id == userId).FirstOrDefaultAsync();
            string text = model.Text + "<br/>";
            text = text + "Клієнт: " + user.UserProfile.Name + "<br/>" + "Телефон: " + user.UserProfile.Phone + "<br/>";
            await _emailService.SendAsync(book.DbUser.Email, model.Subject, text);
            return new MessageApiModel { Message = "Лист відправлено!" };
        }
    }
}