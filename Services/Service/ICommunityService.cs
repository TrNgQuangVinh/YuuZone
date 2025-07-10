using Repositories.DTO.RequestDTO.Community;
using Repositories.DTO.ResponseDTO.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface ICommunityService
    {
        Task<List<CommunityView?>> GetAll();
        Task<CommunityView?> GetDetail(Guid id);
        Task<(CommunityView community, string status)> CreateCommunity(CreateCommunityForm form);
        Task<string> JoinCommunity(JoinCommunityForm form);
        Task<CommunityView?> UpdateCommunity(Guid id, UpdateCommunityForm form);
        Task<string> RemoveCommunity(Guid id);
    }
}
