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
    public class AuthenRepository : GenericRepository<User>, IAuthenRepository
    {
        private readonly IUserRepository _userRepo;

        public AuthenRepository(YuuZoneDbContext dbContext, IUserRepository userRepo) : base(dbContext)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> Login(string input, string password)
        {
            return await _dbContext.Users
                .Where(x => (x.Username.Equals(input) || x.Email.Equals(input)) && x.Password.Equals(password))
                .FirstOrDefaultAsync();
        }

        public async Task<(string status,User? user)> Register(User user)
        {
            try
            {
                var existingUser = await _userRepo.GetUserByUsernameAsync(user.Username);
                if (existingUser != null) return ("User exists with the same username", existingUser);
                existingUser = await _userRepo.GetUserByEmailAsync(user.Email);
                if (existingUser != null) return ("User exists with the same username", existingUser);
                existingUser = await _userRepo.GetUserByPhoneAsync(user.PhoneNumber);
                if (existingUser != null) return ("User exists with the same username", existingUser);

                if (user.RoleId == (int)ConstantEnum.RoleID.ADMIN)
                    user.StatusId = (int)ConstantEnum.StatusID.PENDING;
                if (user.RoleId == (int)ConstantEnum.RoleID.CUSTOMER)
                    user.StatusId = (int)ConstantEnum.StatusID.ACTIVE;

                user.Id = Guid.NewGuid();

                var result = await CreateAsync(user);

                if (result > 0)
                    return (ConstantEnum.RepoStatus.SUCCESS, user);
                else
                    return (ConstantEnum.RepoStatus.FAILURE, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
