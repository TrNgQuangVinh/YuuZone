using Repositories.Base;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IAuthenRepository : IGenericRepository<User>
    {
        Task<User?> Login(string input, string password);
        Task<User?> LoginByGoogle(string email, string name, string googleId);
        Task<(string status ,User user)> Register(User user);
        Task<(string status, User user)> RegisterByGoogle(User user);
    }
}
