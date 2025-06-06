using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IAuthenRepository
    {
        Task<User?> Login(string input, string password);
        Task<(int status ,User user)> Register(User user);
    }
}
