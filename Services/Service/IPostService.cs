using Repositories.DTO.RequestDTO.Post;
using Repositories.DTO.ResponseDTO.Post;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IPostService
    {
        Task<List<PostView?>> GetAllPosts();
        Task<PostView?> GetPostDetail(Guid id);
        Task<(string status, PostView? post)> CreatePostAsync(CreatePostForm form);
        Task<PostView?> UpdatePostAsync(Guid id, UpdatePostForm form);
        Task<string> DeletePostAsync(Guid id);
        Task<List<PostView?>> GetPostsFromCommunity(Guid? communityId, string? communityName);
    }
}
