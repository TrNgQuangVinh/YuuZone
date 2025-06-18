using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.DTO.RequestDTO.User;
using Repositories.DTO.ResponseDTO.User;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> DeleteUserAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._userRepo.DeleteUserAsync(id);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserView?>> GetAllUsers()
        {
            var list = await _unitOfWork._userRepo.GetAllWithIncludeAsync(x => x.Role,
                                                    x => x.Gender,
                                                    x => x.Status);
            return _mapper.Map<List<UserView?>>(list.ToList());
        }

        public async Task<UserView?> GetUserByEmailAsync(string input)
        {
            if (input.IsNullOrEmpty())
                return null;
            var result = await _unitOfWork._userRepo.GetUserByEmailAsync(input);
            return _mapper.Map<UserView>(result);
        }

        public async Task<UserView?> GetUserByPhoneAsync(string input)
        {
            if (input.IsNullOrEmpty())
                return null;
            var result = await _unitOfWork._userRepo.GetUserByPhoneAsync(input);
            return _mapper.Map<UserView>(result);
        }

        public async Task<UserView?> GetUserByUsernameAsync(string input)
        {
            if (input.IsNullOrEmpty())
                return null;
            var result = await _unitOfWork._userRepo.GetUserByUsernameAsync(input);
            return _mapper.Map<UserView>(result);
        }

        public async Task<List<UserView?>> GetUsersWithFilterAsync(string? fullName, string? titleName)
        {
            var list = await _unitOfWork._userRepo.GetUsersWithFilterAsync(fullName, titleName);
            return _mapper.Map<List<UserView?>>(list.ToList());
        }

        public async Task<UserView?> UpdateUserAsync(Guid id, UpdateUserForm userUpd)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existing = _unitOfWork._userRepo.GetById(id);
                if (existing == null)
                    return null;

                //partial mapping
                var user = _mapper.Map(userUpd, existing);
                user = await _unitOfWork._userRepo.UpdateUserAsync(user);

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<UserView>(user);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}