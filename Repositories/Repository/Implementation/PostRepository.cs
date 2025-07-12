using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Constant;
using Repositories.Data;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Post>> GetPostsFromCommunity(Guid communityId)
        {
            return await _dbContext.Posts
                .Include(x => x.Author)
                .Include(x => x.PostVote)
                .Where(x => x.CommunityId == communityId && x.StatusId == (int)ConstantEnum.StatusID.ACTIVE)
                .ToListAsync();
        }

        public async Task<(string status, Post? post)> CreatePostAsync(Post post)
        {
            try
            {
                var result = await CreateAsync(post);

                if (result > 0)
                    return (ConstantEnum.RepoStatus.SUCCESS, post);
                else
                    return (ConstantEnum.RepoStatus.FAILURE, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeletePostAsync(Guid id)
        {
            try
            {
                var existing = await GetByIdAsync(id);
                if (existing == null)
                    return "Post not found";

                await RemoveAsync(existing);

                if (await GetByIdAsync(id) == null)
                    return ConstantEnum.RepoStatus.SUCCESS;
                else
                    return ConstantEnum.RepoStatus.FAILURE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            try
            {
                var result = await UpdateAsync(post);

                return await GetByIdAsync(post.Id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}