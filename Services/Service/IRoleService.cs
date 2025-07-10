using Repositories.DTO.ResponseDTO.Role;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IRoleService
    {
        Task<List<RoleView?>> GetRoles();
        Task<string> AddRoles(string title);
        Task<string> DeleteRoles(int id);
    }
}
