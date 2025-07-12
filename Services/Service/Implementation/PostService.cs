using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories.Base;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.Post;
using Repositories.DTO.ResponseDTO.Post;
using Repositories.DTO.ResponseDTO.User;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class PostService : IPostService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(UnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PostView?>> GetAllPosts()
        {
            var result = await _unitOfWork._postRepo
                .GetAllWithIncludeAsync(x => x.Author,
                                        x => x.Community,
                                        x => x.Status);

            return _mapper.Map<List<PostView?>>(result);
        }

        public async Task<List<PostView?>> GetPostsFromCommunity(Guid? communityId, string? communityName)
        {
            if (!communityId.HasValue && communityName.IsNullOrEmpty())
            {
                return null;
            }
            else if (!communityId.HasValue)
            {
                var existComm = await _unitOfWork._communityRepo.GetCommunityByNameAsync(communityName);
                if(existComm != null)
                {
                    communityId = existComm.Id;
                }
            }
            //This is to convert from Guid? (nullable) to normal Guid cuz those don't mix for some reason
            Guid commId = communityId.Value;
            var result = await _unitOfWork._postRepo.GetPostsFromCommunity(commId);
            return _mapper.Map<List<PostView?>>(result);
        }

        public async Task<PostView?> GetPostDetail(Guid id)
        {
            var result = await _unitOfWork._postRepo
                .GetByIdWithIncludeAsync(id, "Id", x => x.Author,
                                        x => x.Community,
                                        x => x.Status);

            return _mapper.Map<PostView?>(result);
        }

        public async Task<PostView?> UpdatePostAsync(Guid id, UpdatePostForm form)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existing = _unitOfWork._postRepo.GetById(id);
                if (existing == null)
                    return null;

                form.UpdateDate = DateTime.Now;
                //if (form.Status == null)
                  //  form.Status = existing.StatusId;
                //partial mapping
                var post = _mapper.Map(form, existing);
                var result = await _unitOfWork._postRepo.UpdatePostAsync(post);

                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<PostView>(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string status, PostView? post)> CreatePostAsync(CreatePostForm form)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (!form.AuthorId.HasValue && form.AuthorName.IsNullOrEmpty())
                {
                    return ("Author name or id is required", null);
                }
                else if (!form.AuthorId.HasValue)
                {
                    var author = await _unitOfWork._userRepo.GetUserByUsernameAsync(form.AuthorName);
                    form.AuthorId = author.Id;
                }

                if (!form.CommunityId.HasValue && form.CommunityName.IsNullOrEmpty())
                {
                    return ("Community name or id is required", null);
                }
                else if (!form.CommunityId.HasValue)
                {
                    var community = await _unitOfWork._communityRepo.GetCommunityByNameAsync(form.CommunityName);
                    form.CommunityId = community.Id; ;
                }

                var post = _mapper.Map<Post>(form);

                post.PostedDate = DateTime.Now;
                post.UpdateDate = DateTime.Now;

                post.Id = Guid.NewGuid();
                //check for community posting status to know whether a new post need to be reviewed or not
                //technical term: check another status param to know wheter to set new post as active or pending
                post.StatusId = (int)ConstantEnum.StatusID.ACTIVE;

                var result = await _unitOfWork._postRepo.CreatePostAsync(post);
                await _unitOfWork.CommitTransactionAsync();

                var postView = _mapper.Map<PostView>(result.post);

                return (result.status, postView);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeletePostAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._postRepo.DeletePostAsync(id);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}