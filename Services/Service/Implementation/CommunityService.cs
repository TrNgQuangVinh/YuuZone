using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.Constant;
using Repositories.Data.Entities;
using Repositories.DTO.RequestDTO.Community;
using Repositories.DTO.ResponseDTO.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class CommunityService : ICommunityService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommunityService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(CommunityView community, string status)> CreateCommunity(CreateCommunityForm form)
        {
            try
            {
                var existing = await _unitOfWork._communityRepo.GetCommunityByNameAsync(form.Name);

                if (existing != null)
                    return (null, "Community with this name already existed");

                await _unitOfWork.BeginTransactionAsync();

                form.CreateDate = DateTime.Now;

                if (form.StatusId < (int)ConstantEnum.StatusID.PUBLIC)
                {
                    form.StatusId = (int)ConstantEnum.StatusID.PUBLIC;
                }

                var community = _mapper.Map<Community>(form);

                community.Id = Guid.NewGuid();

                var result = await _unitOfWork._communityRepo.CreateCommunityAsync(community);

                await _unitOfWork.CommitTransactionAsync();

                return (_mapper.Map<CommunityView>(result.community), result.status);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CommunityView?>> GetAll()
        {
            var community = await _unitOfWork._communityRepo.GetAllWithIncludeAsync(
                x => x.Tags,
                x => x.Members,
                x => x.Posts);
            return _mapper.Map<List<CommunityView?>>(community.ToList());
        }

        public async Task<CommunityView?> GetDetail(Guid id)
        {
            var community = await _unitOfWork._communityRepo.GetByIdWithIncludeAsync(id, "Id", 
                x => x.Posts,
                x => x.Members,
                x => x.Tags);

            return _mapper.Map<CommunityView?>(community);
        }

        public async Task<string> JoinCommunity(JoinCommunityForm form)
        {
            try
            {
                if (!form.UserId.HasValue && form.Username.IsNullOrEmpty())
                {
                    return "Username or userid is required";
                }
                else if (!form.UserId.HasValue)
                {
                    var user = await _unitOfWork._userRepo.GetUserByUsernameAsync(form.Username);
                    form.UserId = user.Id;
                }

                if (!form.CommunityId.HasValue && form.CommunityName.IsNullOrEmpty())
                {
                    return "Community name or communityid is required";
                }
                else if (!form.CommunityId.HasValue)
                {
                    var community = await _unitOfWork._communityRepo.GetCommunityByNameAsync(form.CommunityName);
                    form.CommunityId = community.Id; ;
                }

                await _unitOfWork.BeginTransactionAsync();

                if (form.CommunityId.HasValue && form.UserId.HasValue)
                {
                    Guid userId = form.UserId.Value;
                    Guid communityId = form.CommunityId.Value;
                    var result = await _unitOfWork._communityRepo.JoinCommunity(userId, communityId);

                    await _unitOfWork.CommitTransactionAsync();
                    return result;
                }
                else return "Something went wrong, check log or contact admin";
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveCommunity(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._communityRepo.RemoveCommunityAsync(id);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<CommunityView?> UpdateCommunity(Guid id, UpdateCommunityForm form)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existing = await _unitOfWork._communityRepo.GetByIdAsync(id);
                if (existing == null)
                    return null;

                var update = _mapper.Map(form, existing);

                var result = await _unitOfWork._communityRepo.UpdateCommunityAsync(update);

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<CommunityView>(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
