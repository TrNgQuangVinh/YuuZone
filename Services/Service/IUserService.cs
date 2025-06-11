using Repositories.DTO.RequestDTO;
using Repositories.DTO.ResponseDTO;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IUserService
    {
        Task<List<UserView>> GetAllUsers();
        Task<UserView?> GetUserByEmailAsync(string input);
        Task<UserView?> GetUserByUsernameAsync(string input);
        Task<UserView?> GetUserByPhoneAsync(string input);
        Task<List<UserView>> GetUsersWithFilterAsync(string? fullName, string? titleName);

        Task<UserView?> UpdateUserAsync(Guid id, UpdateUserForm user);
        Task<string> DeleteUserAsync(Guid id);
    }
}
