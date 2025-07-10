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
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
                .Where(x => (x.Username.Equals(input) || x.Email.Equals(input)) 
                && x.Password == password)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        //Both Login and GoogleLogin here uses == operator instead of .Equals
        //The reason is for null safety (google registered account has no password unless explicitly set)
        //Avoid SqlNullValueException
        //it cannot translate the instance‑method call string.Equals(string) when 
        //the left‑hand side might be null and the right‑hand side is a captured variable.

        public async Task<User?> LoginByGoogle(string email, string name, string googleId)
        {
            
            return await _dbContext.Users
                .Where(x => (x.Email.Equals(email) || x.Username.Equals(name)) 
                && x.GoogleId == googleId 
                && x.IsGoogle == true)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<(string status,User? user)> Register(User user)
        {
            try
            {
                var existingUser = await _userRepo.GetUserByUsernameAsync(user.Username);
                if (existingUser != null) return ("User exists with the same username", existingUser);
                existingUser = await _userRepo.GetUserByEmailAsync(user.Email);
                if (existingUser != null) return ("User exists with the same email", existingUser);
                existingUser = await _userRepo.GetUserByPhoneAsync(user.PhoneNumber);
                if (existingUser != null) return ("User exists with the same phone number", existingUser);

                if (user.RoleId == (int)ConstantEnum.RoleID.ADMIN)
                    user.StatusId = (int)ConstantEnum.StatusID.PENDING;
                if (user.RoleId == (int)ConstantEnum.RoleID.CUSTOMER)
                    user.StatusId = (int)ConstantEnum.StatusID.ACTIVE;

                user.Id = Guid.NewGuid();
                user.IsGoogle = false;

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

        public async Task<(string status, User? user)> RegisterByGoogle(User user)
        {
            try
            {
                //exist check is in services layer instead

                user.RoleId = (int)ConstantEnum.RoleID.CUSTOMER;
                user.StatusId = (int)ConstantEnum.StatusID.ACTIVE;

                user.Id = Guid.NewGuid();
                user.IsGoogle = true;
                user.GenderId = (int)ConstantEnum.GenderID.OTHER;

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
