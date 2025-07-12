using Repositories.DTO.RequestDTO.Comment;
using Repositories.DTO.ResponseDTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface ICommentService
    {
        Task<List<CommentView?>> GetCommentsByPost(Guid id);
        Task<(string status, CommentView? comment)> AddComment(CreateCommentForm form);
        Task<CommentView> UpdateComment(Guid id, string content);
        Task<string> RemoveComment(Guid id);
    }
}
