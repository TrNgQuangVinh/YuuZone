using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.User;
using Repositories.DTO.ResponseDTO.User;
using Repository.Data.Entities;
using Services.Service;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServ;

        public UserController(IUserService userServ)
        {
            _userServ = userServ;
        }
        //Unified search endpoint
        [HttpGet("search")]
        public async Task<IActionResult> GetByEmail([FromQuery] string? email, [FromQuery] string? phone, [FromQuery] string? userName)
        {
            try
            {
                List<UserView?> result = new();
                if (!email.IsNullOrEmpty())
                    result.Add(await _userServ.GetUserByEmailAsync(email));

                if (!phone.IsNullOrEmpty())
                    result.Add(await _userServ.GetUserByPhoneAsync(phone));

                if (!userName.IsNullOrEmpty())
                    result.Add(await _userServ.GetUserByUsernameAsync(userName));

                if (email.IsNullOrEmpty() && phone.IsNullOrEmpty() && userName.IsNullOrEmpty())
                    result = await _userServ.GetAllUsers();

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

        [HttpGet("email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string? email)
        {
            try
            {
                var result = await _userServ.GetUserByEmailAsync(email);
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

        [HttpGet("phone")]
        public async Task<IActionResult> GetByPhone([FromQuery] string? phone)
        {
            try
            {
                var result = await _userServ.GetUserByPhoneAsync(phone);
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

        [HttpGet("username")]
        public async Task<IActionResult> GetByUsername([FromQuery] string? userName)
        {
            try
            {
                var result = await _userServ.GetUserByUsernameAsync(userName);
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

        [HttpGet("list-filter")]
        public async Task<IActionResult> GetListWithFilters([FromQuery] string? fullName, [FromQuery] string? titleName)
        {
            try
            {
                var result = await _userServ.GetUsersWithFilterAsync(fullName, titleName);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserForm user)
        {
            try
            {
                var result = await _userServ.UpdateUserAsync(id, user);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Update failed, check log"
                    })
                    : Ok(result);
            }
            catch(Exception ex)
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
                var result = await _userServ.DeleteUserAsync(id);
                return result.Equals(ConstantEnum.RepoStatus.SUCCESS)
                    ? Ok(result)
                    : StatusCode(500, new
                    {
                        Message = result
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
