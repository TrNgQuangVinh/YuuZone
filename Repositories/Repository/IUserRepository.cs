using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string input);
        Task<User> GetUserByUsernameAsync(string input);
        Task<User> GetUserByPhoneAsync(string input);
    }
}
