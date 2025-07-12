using Repositories.Base;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<(string status, Post post)> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task<string> DeletePostAsync(Guid id);
        Task<List<Post?>> GetPostsFromCommunity(Guid communityId);
    }
}
