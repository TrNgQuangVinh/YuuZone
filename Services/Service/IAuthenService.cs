using Repositories.DTO.RequestDTO.User;
using Repositories.DTO.ResponseDTO.User;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IAuthenService
    {
        Task<(UserLoginView? login, UserPostRegView? register)> GoogleLogin(string email, string name, string googleId);
        Task<UserLoginView?> Login(string? input, string password);
        Task<(string status, UserPostRegView? user)> Register(RegisterUserForm regUser);
    }
}
