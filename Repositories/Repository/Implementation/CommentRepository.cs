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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(string status, Comment? comment)> AddComment(Comment comment)
        {
            try
            {
                var result = await CreateAsync(comment);
                if (result > 0)
                    return (ConstantEnum.RepoStatus.SUCCESS, comment);
                else
                    return (ConstantEnum.RepoStatus.FAILURE, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId)
        {
            return await _dbContext.Comments
                    .Where(x => x.PostId.Equals(postId))
                    .Include(x => x.Author)
                    .OrderByDescending(x => x.PostedDate)
                    .AsNoTracking()
                    .ToListAsync();
        }


        public async Task<string> RemoveComment(Guid id)
        {
            try
            {
                var existing = await GetByIdAsync(id);
                if (existing == null)
                    return "Comment not found";

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

        public async Task<Comment> UpdateComment(Comment comment)
        {
            try
            {
                var result = await UpdateAsync(comment);

                return await GetByIdAsync(comment.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
