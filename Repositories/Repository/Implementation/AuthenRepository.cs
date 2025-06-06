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
    public class AuthenRepository : Repository<User>, IAuthenRepository
    {
        private readonly IUserRepository _userRepo;

        public AuthenRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> Login(string input, string password)
        {
            return await _dbContext.Users
                .Where(x => (x.Username.Equals(input) || x.Email.Equals(input)) && x.Password.Equals(password))
                .FirstOrDefaultAsync();
        }

        public async Task<(int status,User? user)> Register(User user)
        {
            /*
             *  0 = account already exist
             *  1 = account registered successfully
             *  -1 = account register failure
             */
            var existingUser = await _dbContext.Users
                .Where(x => x.Username.Equals(user.Username) 
                || x.Email.Equals(user.Email) 
                || x.PhoneNumber.Equals(user.PhoneNumber))
                .FirstOrDefaultAsync();
            if (existingUser != null) return (0,existingUser); //user already exist with same name, email or phone

            if (user.RoleId == (int)ConstantEnum.RoleID.ADMIN)
                user.StatusId = (int)ConstantEnum.StatusID.PENDING;
            if (user.RoleId == (int)ConstantEnum.RoleID.CUSTOMER)
                user.StatusId = (int)ConstantEnum.StatusID.ACTIVE;
            user.Id = Guid.NewGuid();
            var result = await CreateAsync(user);
            if (result > 0)
                return (result, user);
            else
                return (result, null);
        }
    }
}
