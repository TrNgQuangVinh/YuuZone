using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repositories.DTO.RequestDTO;
using Repositories.DTO.ResponseDTO;
using Repositories.Repository;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class AuthenService : IAuthenService
    {
        private readonly IAuthenRepository _repo;
        private readonly IMapper _mapper;

        public AuthenService(IAuthenRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserPostRegView?> Login(string? input, string password)
        {
            if (input.IsNullOrEmpty())
                return null;
            var user = await _repo.Login(input, password);
            return _mapper.Map<UserPostRegView>(user);
        }

        public async Task<(int status,UserPostRegView? user)> Register(RegisterUserForm regUser)
        {
            var regData = _mapper.Map<User>(regUser);
            var response = await _repo.Register(regData);
            return response.status > 0
                ? (response.status, _mapper.Map<UserPostRegView>(response.user))
                : (response.status, null);
        }
    }
}
