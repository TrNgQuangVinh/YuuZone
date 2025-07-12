using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Entities
{
    public class PostVote
    {
        //Composite key defined in dbContext
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }

        [ForeignKey("UserId")]
        public virtual User Voters { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Voted { get; set; }


    }
}
