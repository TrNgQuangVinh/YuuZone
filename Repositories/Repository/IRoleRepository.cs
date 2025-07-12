using Repositories.Base;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<string> AddRoles(string title);
        Task<string> DeleteRoles(int id);
    }
}
