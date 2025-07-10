using Microsoft.AspNetCore.Mvc;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.Community;
using Services.Service;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityServ;

        public CommunityController(ICommunityService communityServ)
        {
            _communityServ = communityServ;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _communityServ.GetAll();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            try
            {
                var result = await _communityServ.GetDetail(id);
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
        public async Task<IActionResult> Create([FromBody] CreateCommunityForm form)
        {
            try
            {
                var result = await _communityServ.CreateCommunity(form);
                return result.community == null
                    ? StatusCode(500, new
                    {
                        Message = "Create failed, check log",
                        InnerMessage = result.status
                    })
                    : Ok(new 
                    { 
                        Message = result.status,
                        Community = result.community          
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

        [HttpGet("join")]
        public async Task<IActionResult> JoinCommunity([FromQuery] JoinCommunityForm form)
        {
            try
            {
                var result = await _communityServ.JoinCommunity(form);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Join failed, check log"
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommunityForm form)
        {
            try
            {
                var result = await _communityServ.UpdateCommunity(id, form);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _communityServ.RemoveCommunity(id);
                return result.Equals(ConstantEnum.RepoStatus.FAILURE)
                    ? StatusCode(500, new
                    {
                        Message = "Delete failed, check log"
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
