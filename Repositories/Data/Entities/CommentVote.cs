using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Entities
{
    public class CommentVote
    {
        //Composite key defined in dbContext
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }

        [ForeignKey("UserId")]
        public User Voters { get; set; }
        [ForeignKey("CommentId")]
        public Comment Voted { get; set; }
    }
}
