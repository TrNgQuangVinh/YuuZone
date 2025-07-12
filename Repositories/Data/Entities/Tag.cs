using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Entities
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public string TagTitle { get; set; }

        public virtual ICollection<Community>? Communities { get; set; } = new List<Community>();
    }
}
