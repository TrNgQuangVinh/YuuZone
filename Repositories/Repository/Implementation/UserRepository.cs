using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Data;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
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
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByPhoneAsync(string input)
        {
            return await _dbContext.Users
                .Where(x => x.PhoneNumber.Equals(input))
                .Include(x => x.Role)
                .Include(x => x.Gender)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string input)
        {
            return await _dbContext.Users
                .Where(x => x.Username.Equals(input))
                .Include(x => x.Role)
                .Include(x => x.Gender)
                .FirstOrDefaultAsync();
        }
    }
}
