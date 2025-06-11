using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.Constant;
using Repositories.Data;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User?> GetUserByEmailAsync(string input)
        {
            return await _dbContext.Users
                .Where(x => x.Email.Equals(input))
                .Include(x => x.Role)
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByPhoneAsync(string input)
        {
            return await _dbContext.Users
                .Where(x => x.PhoneNumber.Equals(input))
                .Include(x => x.Role)
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string input)
        {
            return await _dbContext.Users
                .Where(x => x.Username.Equals(input))
                .Include(x => x.Role)
                .Include(x => x.Gender)
                .Include(x => x.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersWithFilterAsync(string? fullName, string? titleName)
        {
            if (!fullName.IsNullOrEmpty() || !titleName.IsNullOrEmpty())
                return await _dbContext.Users
                    .Where(x => x.Fullname.Contains(fullName)
                        || x.TitleName.Contains(titleName))
                    .Include(x => x.Role)
                    .Include(x => x.Gender)
                    .Include(x => x.Status)
                    .ToListAsync();
            else
            {
                return await GetAllWithIncludeAsync(x => x.Role,
                                                    x => x.Gender,
                                                    x => x.Status);
            }
                
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user)
        {
            try
            {
                var existing = await GetByIdAsync(id);
                if (existing == null)
                    return null;

                user.Id = id;
                user.Password = existing.Password;
                user.StatusId = existing.StatusId;
                user.RoleId = existing.RoleId;
                var result = await UpdateAsync(user);

                return await GetByIdAsync(user.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<string> DeleteUserAsync(Guid id)
        {
            try
            {
                var existing = await GetByIdAsync(id);
                if (existing == null)
                    return "No account found!";

                await RemoveAsync(existing);

                if (await GetByIdAsync(id) == null)
                    return ConstantEnum.RepoStatus.SUCCESS;
                else
                    return ConstantEnum.RepoStatus.FAILURE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
