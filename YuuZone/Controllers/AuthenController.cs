using Microsoft.AspNetCore.Mvc;
using Repositories.DTO.RequestDTO;
using Services.Service;

namespace YuuZone.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authServ;

        public AuthenController(IAuthenService authServ)
        {
            _authServ = authServ;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string? emailOrPassword, string password)
        {
            try
            {
                var result = await _authServ.Login(emailOrPassword, password);
                return result == null
                    ? NotFound()
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserForm regUser)
        {
            try
            {
                var result = await _authServ.Register(regUser);
                return result.status > 0
                    ? Ok(regUser)
                    : StatusCode(409, new
                    {
                        Message = "Username, Email or Phone number already registered!"
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
    }
}
