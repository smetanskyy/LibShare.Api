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
        public async Task<IdentityResult> Create(DbUser item, string password)
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

        public async Task<bool> Delete(string id)
        {
            DbUser user = _context.Users.Find(id);
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<DbUser>> Find(Expression<Func<DbUser, bool>> predicate)
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

        public async Task<DbUser> GetByEmail(string email)
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

        public async Task<IEnumerable<DbUser>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DbUser> GetById(string id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Update(DbUser item)
        {
            try
            {
                _context.Users.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserToken(DbUser user, string refreshToken)
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
    }
}
