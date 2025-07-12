using Repositories.Base;
using Repositories.Data.Entities;
using Repositories.DTO.ResponseDTO.Comment;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment?>> GetCommentsByPost(Guid postId);
        Task<(string status, Comment? comment)> AddComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
        Task<string> RemoveComment(Guid id);
    }
}
