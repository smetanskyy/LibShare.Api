using LibShare.Api.Data.Constants;
using LibShare.Api.Data.Entities;
using LibShare.Api.Data.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibShare.Api.Data.Repositories
{
    public class UserRepository : ICrudRepository<DbUser, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<DbUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<DbUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateAsync(DbUser item, string password)
        {
            if (item == null) return null;
            try
            {
                var userRoleName = Roles.User;
                var result = await _userManager.CreateAsync(item, password);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(item, userRoleName);
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string id, string deletionReason)
        {
            DbUser user = _context.Users.Find(id);
            try
            {
                user.IsDeleted = true;
                var record = new AccessProhibited { DateDelete = DateTime.Now, Id = id, DeletionReason = deletionReason };
                _context.AccessProhibited.Add(record);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<DbUser>> FindAsync(Expression<Func<DbUser, bool>> predicate)
        {
            try
            {
                return await _context.Users.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<DbUser> GetByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DbUser>> GetAllAsync()
        {
            try
            {
                return await _context.Users.Include(x => x.UserProfile).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DbUser> GetByIdAsync(string id)
        {
            try
            {
                return await _context.Users.Include(x => x.UserProfile).SingleAsync(u => u.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(DbUser item)
        {
            try
            {
                await _userManager.UpdateAsync(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserTokenAsync(DbUser user, string refreshToken)
        {
            if (user == null) return false;

            var tokendb = _context.Tokens.Find(user.Id);

            if (tokendb == null)
            {
                _context.Tokens.Add(new Token
                {
                    Id = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
                });
            }
            else
            {
                tokendb.RefreshToken = refreshToken;
                tokendb.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _context.Tokens.Update(tokendb);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> UpdateUserPhotoAsync(DbUser user, string photo)
        {
            if (user == null || string.IsNullOrWhiteSpace(photo)) return false;

            var userProfile = _context.UserProfile.Find(user.Id);

            if (userProfile == null)
            {
                userProfile = new UserProfile { Id = user.Id, RegistrationDate = DateTime.Now };
            }

            userProfile.Photo = photo;
            _context.UserProfile.Update(userProfile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
