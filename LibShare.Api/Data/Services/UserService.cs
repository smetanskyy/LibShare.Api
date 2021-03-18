using AutoMapper;
using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
using LibShare.Api.Infrastructure.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class UserService : IUserService<UserApiModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<DbUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ResourceManager _resourceManager;

        public UserService(IUserRepository userRepository,
            UserManager<DbUser> userManager,
            IMapper mapper,
            IWebHostEnvironment env,
            IConfiguration configuration,
            ResourceManager resourceManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _env = env;
            _configuration = configuration;
            _resourceManager = resourceManager;
        }

        public async Task<ImageApiModel> UpdateUserPhotoAsync(ImageApiModel model, string userId, HttpRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"{_resourceManager.GetString("UserNotFound")}: {userId}");
            }

            string base64 = model.Photo;
            if (base64.Contains(","))
            {
                base64 = base64.Split(',')[1];
            }

            var serverPath = _env.ContentRootPath; //Directory.GetCurrentDirectory(); //_env.WebRootPath;
            var folderName = _configuration.GetValue<string>("ImagesPath");
            var path = Path.Combine(serverPath, folderName);

            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            string ext = ".jpg";
            string fileName = Guid.NewGuid().ToString("D") + ext;
            string filePathSave = Path.Combine(path, fileName);

            string filePathDelete = null;
            if (user.UserProfile != null && user.UserProfile.Photo != null)
                filePathDelete = Path.Combine(path, user.UserProfile.Photo);

            //Convert Base64 Encoded string to Byte Array.
            byte[] imageBytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePathSave, imageBytes);

            var result = await _userRepository.UpdateUserPhotoAsync(user, fileName);

            if (!result)
            {
                throw new BadRequestException(_resourceManager.GetString("PhotoNotChanged"));
            }

            if (File.Exists(filePathDelete))
            {
                File.Delete(filePathDelete);
            }

            string pathPhoto = $"{request.Scheme}://{request.Host}/{_configuration.GetValue<string>("UrlImages")}/";
            fileName = fileName != null ? pathPhoto + fileName : null;

            return new ImageApiModel { Photo = fileName };
        }

        public async Task<UserApiModel> CreateUserAsync(UserApiModel model, string password)
        {
            var searchUser = _userManager.FindByEmailAsync(model.Email).Result;

            if (searchUser != null && searchUser.IsDeleted == true)
            {
                throw new UserIsDeletedException(_resourceManager.GetString("UserIsDeleted"));
            }

            if (searchUser != null)
            {
                throw new BadRequestException(_resourceManager.GetString("EmailExist"));
            }

            var userByUsername = _userManager.FindByNameAsync(model.UserName).Result;
            if (userByUsername != null)
            {
                throw new BadRequestException(_resourceManager.GetString("UsernameExist"));
            }

            var user = new DbUser();
            user.Email = model.Email;
            user.UserName = model.UserName;

            user.UserProfile = user.UserProfile ?? new UserProfile();

            user.UserProfile.Name = model.Name;
            user.UserProfile.Surname = model.Surname;
            user.UserProfile.Phone = model.Phone;
            user.UserProfile.DateOfBirth = model.DateOfBirth;
            user.UserProfile.Photo = model.Photo;

            var resultCreated = await _userRepository.CreateAsync(user, password);
            if (!resultCreated.Succeeded)
            {
                throw new BadRequestException(resultCreated.Errors.First().Description);
            }

            var result = _mapper.Map<UserApiModel>(user);
            return result;
        }

        public async Task<UserApiModel> DeleteUserByIdAsync(string id, string deletionReason)
        {
            var user = await _userRepository.DeleteAsync(id, deletionReason);
            return _mapper.Map<UserApiModel>(user);
        }

        public Task<IEnumerable<UserApiModel>> FindUserAsync(Expression<Func<UserApiModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserApiModel> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            if (users.Count() < 1)
            {
                throw new NotFoundException(_resourceManager.GetString("UsersNotFound"));
            }

            return _mapper.Map<IEnumerable<UserApiModel>>(users);
        }

        public async Task<UserApiModel> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"{_resourceManager.GetString("UserNotFound")}: {id}");
            }
            var result = _mapper.Map<UserApiModel>(user);
            return result;
        }

        public async Task<UserApiModel> GetUserByIdWithFullPhotoUrlAsync(string userId, HttpRequest request)
        {
            var result = await GetUserByIdAsync(userId);
            string pathPhoto = $"{request.Scheme}://{request.Host}/{_configuration.GetValue<string>("UrlImages")}/";
            result.Photo = result.Photo != null ? pathPhoto + result.Photo : null;
            return result;
        }

        public async Task<ImageApiModel> GetUserPhotoAsync(string userId, HttpRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            string pathPhoto = $"{request.Scheme}://{request.Host}/{_configuration.GetValue<string>("UrlImages")}/";
            string imagePath = user?.UserProfile?.Photo != null ? pathPhoto + user.UserProfile.Photo : null;

            return new ImageApiModel { Photo = imagePath };
        }

        public async Task<UserApiModel> UpdateUserAsync(UserApiModel model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);
            if (user == null)
            {
                throw new NotFoundException($"{_resourceManager.GetString("UserNotFound")}: {model.Id}");
            }

            // Check email (if new email exist in another user)
            if (user.Email != model.Email)
            {
                var searchUser = await _userManager.FindByEmailAsync(model.Email);
                if (searchUser != null && searchUser.Id != model.Id)
                    throw new ArgumentException(_resourceManager.GetString("EmailExist"));
            }

            // Check userName (if new userName exist in another user)
            if (user.UserName != model.UserName)
            {
                var searchUser = await _userManager.FindByNameAsync(model.UserName);
                if (searchUser != null && searchUser.Id != model.Id)
                    throw new ArgumentException(_resourceManager.GetString("UsernameExist"));
            }

            user.Email = model.Email;
            user.UserName = model.UserName;

            user.UserProfile = user.UserProfile ?? new UserProfile();

            user.UserProfile.Name = model.Name;
            user.UserProfile.Surname = model.Surname;
            user.UserProfile.Phone = model.Phone;
            user.UserProfile.DateOfBirth = model.DateOfBirth;

            //if (!string.IsNullOrWhiteSpace(model.Photo))
            //    user.UserProfile.Photo = model.Photo;

            await _userRepository.UpdateAsync(user);
            var result = _mapper.Map<UserApiModel>(user);
            return result;
        }
    }
}
