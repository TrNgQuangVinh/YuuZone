using Repositories.Base;
using Repositories.Data;
using Repositories.Data.Entities;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Implementation
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(YuuZoneDbContext dbContext) : base(dbContext)
        {
        }
    }
}
