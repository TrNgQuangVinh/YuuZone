using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Constant;
using Repositories.DTO.RequestDTO.User;
using Services.Service;
using Swashbuckle.AspNetCore.Annotations;

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
        [Authorize(Roles = "1")]
        [HttpGet("jwtRoles1")]
        [SwaggerOperation(Summary = "Should throw 401 Unauth if no jwt, 403 Forbid if role is not 1")]
        public async Task<IActionResult> JWTTestRoles()
        {
            return Ok("Welcome back Captain");
        }

        [Authorize]
        [HttpGet("jwt")]
        [SwaggerOperation(Summary = "Should throw 401 Unauth if no jwt")]
        public async Task<IActionResult> JWTTest()
        {
           return Ok("Hello User");
        }

        /// <param name="localURL">https://localhost:7184/Authen/login/google</param>
        [SwaggerOperation(Summary = "Copy the local url and open it on a new tab")]
        [HttpGet("login/google")]
        public async Task<IActionResult> GoogleLogin(string? localURL)
        {
            var redirUrl = Url.Action("GoogleResponse", "Authen", null);
            var request = new AuthenticationProperties { RedirectUri = redirUrl };
            return Challenge(request, "Google");
        }

        /// <summary>Don't run this</summary>
        [HttpGet("google-callback")] // This must match the authorized redir uri in google cloud console oauth client
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync("Google");
                if (!result.Succeeded)
                    return Unauthorized();
                if (result.Principal == null)
                    return Unauthorized();

                var email = result.Principal.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
                var name = result.Principal.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
                var googleId = result.Principal.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (email.IsNullOrEmpty())
                    return BadRequest();
                //service LoginGoogle if no acc regster Google take it email nam googleiD
                var response = await _authServ.GoogleLogin(email,name,googleId);
                //check response.Status
                if (response.login == null && response.register == null)
                    throw new Exception("Something went wrong, contact admin");
                else if (response.login != null)
                    return Ok(response.login);
                else if (response.register != null)
                    return Ok(response.register);
                else
                    throw new Exception("Something went wrong, contact admin");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string? emailOrUsername, string password)
        {
            try
            {
                var result = await _authServ.Login(emailOrUsername, password);
                return result == null
                    ? NotFound(new
                    {
                        Message = "Account not found"
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserForm regUser)
        {
            try
            {
                var result = await _authServ.Register(regUser);
                return result.status.Equals(ConstantEnum.RepoStatus.SUCCESS)
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
