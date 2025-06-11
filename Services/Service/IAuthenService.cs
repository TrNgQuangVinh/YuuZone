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
    public interface IAuthenService
    {
        Task<UserPostRegView> Login(string? input, string password);
        Task<(string status, UserPostRegView user)> Register(RegisterUserForm regUser);
    }
}
