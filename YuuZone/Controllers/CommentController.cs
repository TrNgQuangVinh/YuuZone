using Microsoft.AspNetCore.Mvc;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.Comment;
using Services.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentServ;

        public CommentController(ICommentService commentServ)
        {
            _commentServ = commentServ;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Accepted input for sort is \"new\", \"top\" and maybe \"hot\" (vote is not impl yet)")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            try
            {
                var result = await _commentServ.GetCommentsByPost(postId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentForm form)
        {
            try
            {
                var result = await _commentServ.AddComment(form);
                return result.status.Equals(ConstantEnum.RepoStatus.SUCCESS)
                    ? StatusCode(500, new
                    {
                        Message = result.status
                    })
                    : Ok(result.comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateComment([FromQuery] Guid id, [FromBody] string content)
        {
            try
            {
                var result = await _commentServ.UpdateComment(id, content);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            try
            {
                var result = await _commentServ.RemoveComment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

    }
}
