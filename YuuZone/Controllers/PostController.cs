using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.Post;
using Services.Service;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postServ;

        public PostController(IPostService postServ)
        {
            _postServ = postServ;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _postServ.GetAllPosts();
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Internal server error, check log"
                    })
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("community")]
        public async Task<IActionResult> GetPostFromCommunity(Guid? communityId, string? communityName)
        {
            try
            {
                var result = await _postServ.GetPostsFromCommunity(communityId, communityName);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Internal server error, check log"
                    })
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            try
            {
                var result = await _postServ.GetPostDetail(id);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Internal server error, check log"
                    })
                    : Ok(result);
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
        public async Task<IActionResult> Create([FromBody] CreatePostForm form)
        {
            try
            {
                if((form.AuthorName.IsNullOrEmpty() && !form.AuthorId.HasValue) 
                    || (form.CommunityName.IsNullOrEmpty() && !form.CommunityId.HasValue))
                    return StatusCode(400, new
                    {
                        Message = "There must be an Author and target Community, name or id (higher priority) is accepted"
                    });
                var result = await _postServ.CreatePostAsync(form);
                return result.status.Equals(ConstantEnum.RepoStatus.SUCCESS)
                    ? Ok(result.post)
                    : StatusCode(500, new
                    {
                        Message = result.status
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostForm form)
        {
            try
            {
                var result = await _postServ.UpdatePostAsync(id, form);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Update failed, check log"
                    })
                    : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _postServ.DeletePostAsync(id);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = result
                    })
                    : Ok(result);
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
