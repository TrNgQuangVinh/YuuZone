using AutoMapper;
using Microsoft.Extensions.Logging;
using Repositories.Base;
using Repositories.DTO.RequestDTO.Comment;
using Repositories.DTO.ResponseDTO.Comment;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentService> _logger;

        public CommentService(UnitOfWork unitOfWork, IMapper mapper, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CommentView?>> GetCommentsByPost(Guid id)
        {
            var comment = await _unitOfWork._commentRepo.GetCommentsByPost(id);
            return _mapper.Map<List<CommentView>>(comment);
        }

        public async Task<(string status, CommentView? comment)> AddComment(CreateCommentForm form)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existing = _unitOfWork._postRepo.GetByIdAsync(form.PostId);
                if (existing == null)
                    return ("No post with provided ID exists!", null);

                var comment = _mapper.Map<Comment>(form);

                comment.Id = Guid.NewGuid();
                comment.PostedDate = DateTime.Now;

                var result = await _unitOfWork._commentRepo.AddComment(comment);
                await _unitOfWork.CommitTransactionAsync();

                var commentView = _mapper.Map<CommentView>(result.comment);

                return (result.status, commentView);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveComment(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var result = await _unitOfWork._commentRepo.RemoveComment(id);

                await _unitOfWork.CommitTransactionAsync();

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<CommentView> UpdateComment(Guid id, string content)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existing = await _unitOfWork._commentRepo.GetByIdAsync(id);
                if (existing == null)
                    return null;

                existing.Content = content;

                var result = await _unitOfWork._commentRepo.UpdateComment(existing);

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<CommentView>(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
