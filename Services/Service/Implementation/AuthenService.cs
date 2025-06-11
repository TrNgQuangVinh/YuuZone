using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.Constant;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthenService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserPostRegView?> Login(string? input, string password)
        {
            if (input.IsNullOrEmpty())
                return null;
            var user = await _unitOfWork._authRepo.Login(input, password);
            return _mapper.Map<UserPostRegView>(user);
        }

        public async Task<(string status,UserPostRegView? user)> Register(RegisterUserForm regUser)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                regUser.PhoneNumber = regUser.PhoneNumber.Trim();
                var regData = _mapper.Map<User>(regUser);
                var response = await _unitOfWork._authRepo.Register(regData);

                await _unitOfWork.CommitTransactionAsync();

                return response.status.Equals(ConstantEnum.RepoStatus.SUCCESS)
                    ? (response.status, _mapper.Map<UserPostRegView>(response.user))
                    : (response.status, null);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
            
        }
    }
}
