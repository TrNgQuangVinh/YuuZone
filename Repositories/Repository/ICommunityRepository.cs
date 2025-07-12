using Repositories.Base;
using Repositories.Data.Entities;
using Repositories.DTO.RequestDTO.Community;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface ICommunityRepository : IGenericRepository<Community>
    {
        Task<Community?> GetCommunityByNameAsync(string? communityName);
        Task<(string status, Community? community)> CreateCommunityAsync(Community community);
        Task<Community?> UpdateCommunityAsync(Community community);
        Task<string> RemoveCommunityAsync(Guid id);
        Task<string> JoinCommunity(Guid userId, Guid communityId);
    }
}
