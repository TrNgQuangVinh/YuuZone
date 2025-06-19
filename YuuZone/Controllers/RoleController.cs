using Microsoft.AspNetCore.Mvc;
using Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YuuZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleServ;

        public RoleController(IRoleService roleServ)
        {
            _roleServ = roleServ;
        }


        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _roleServ.GetRoles();
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

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string value)
        {
            try
            {
                var result = await _roleServ.AddRoles(value);
                return result == null
                    ? StatusCode(500, new
                    {
                        Message = "Create failed, check log"
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

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _roleServ.DeleteRoles(id);
                return result == null
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
