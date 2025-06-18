using Microsoft.AspNetCore.Mvc;
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

        
    }
}
