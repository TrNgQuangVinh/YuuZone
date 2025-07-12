using Repositories.Base;
using Repositories.Constant;
using Repositories.Data;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> AddRoles(string title)
        {
            try
            {
                Role role = new Role { Name = title };
                var result = await CreateAsync(role);
                return result > 0
                    ? ConstantEnum.RepoStatus.SUCCESS
                    : ConstantEnum.RepoStatus.FAILURE;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteRoles(int id)
        {
            var role = await GetByIdAsync(id);
            var result = await RemoveAsync(role);
            return result
                ? ConstantEnum.RepoStatus.SUCCESS
                : ConstantEnum.RepoStatus.FAILURE;
        }
    }
}
