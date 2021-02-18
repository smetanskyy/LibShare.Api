using AutoMapper;
using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.DTO;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces;
using LibShare.Api.Data.Interfaces.IRepositories;
using LibShare.Api.Infrastructure.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Services
{
    public class UserService : IUserService<UserApiModel>
    {
        private readonly ICrudRepository<DbUser, string> _userRepository;
        private readonly UserManager<DbUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ResourceManager _resourceManager;

        public UserService(ICrudRepository<DbUser, string> userRepository,
            UserManager<DbUser> userManager,
            IMapper mapper,
            IEmailService emailService,
            IWebHostEnvironment env,
            IConfiguration configuration,
            ResourceManager resourceManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
            _env = env;
            _configuration = configuration;
            _resourceManager = resourceManager;
        }

        public Task<UserApiModel> CreateUserAsync(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserApiModel>> FindUserAsync(Expression<Func<UserApiModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserApiModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users.Count() < 1)
            {
                throw new NotFoundException(_resourceManager.GetString("UsersNotFound"));
            }

            return _mapper.Map<IEnumerable<UserApiModel>>(users);
        }

        public Task<UserApiModel> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserApiModel user)
        {
            throw new NotImplementedException();
        }
    }
}
