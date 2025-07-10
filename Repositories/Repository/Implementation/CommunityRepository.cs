using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Constant;
using Repositories.Data;
using Repositories.Data.Entities;
using Repositories.DTO.RequestDTO.Community;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class CommunityRepository : GenericRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Community?> GetCommunityByNameAsync(string communityName)
        {
            return await _dbContext.Communities
                .Where(x => x.Name.Equals(communityName))
                .Include(x => x.Status)
                .Include(x => x.Tags)
                .Include(x => x.Posts)
                .Include(x => x.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<(string status, Community? community)> CreateCommunityAsync(Community community)
        {
            try
            {
                var result = await CreateAsync(community);

                if (result > 0)
                    return (ConstantEnum.RepoStatus.SUCCESS, community);
                else
                    return (ConstantEnum.RepoStatus.FAILURE, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveCommunityAsync(Guid id)
        {
            try
            {
                var existing = await GetByIdAsync(id);
                if (existing == null)
                    return "Post not found";

                await RemoveAsync(existing);

                if (await GetByIdAsync(id) == null)
                    return ConstantEnum.RepoStatus.SUCCESS;
                else
                    return ConstantEnum.RepoStatus.FAILURE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Community?> UpdateCommunityAsync(Community community)
        {
            try
            {
                var result = await UpdateAsync(community);

                return await GetByIdAsync(community.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> JoinCommunity(Guid userId, Guid communityId)
        {
            try
            {
                // create “stub” entities so we don't have to hit the DB
                var community = new Community { Id = communityId };
                var user = new User { Id = userId };

                _dbContext.AttachRange(community, user); // mark them as existing

                community.Members.Add(user); // creates the link
                await _dbContext.SaveChangesAsync();
                return ConstantEnum.RepoStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
