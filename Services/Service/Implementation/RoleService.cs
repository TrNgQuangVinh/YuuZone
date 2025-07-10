using AutoMapper;
using Repositories.Base;
using Repositories.Constant;
using Repositories.DTO.ResponseDTO.Comment;
using Repositories.DTO.ResponseDTO.Role;
using Repositories.Repository;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> AddRoles(string title)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._roleRepo.AddRoles(title);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteRoles(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._roleRepo.DeleteRoles(id);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RoleView?>> GetRoles()
        {
            var result = await _unitOfWork._roleRepo.GetAllWithIncludeAsync(x => x.Users);
            return _mapper.Map<List<RoleView?>>(result.ToList());
        }
    }
}