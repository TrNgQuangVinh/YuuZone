using Repositories.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string CommunityId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        [ForeignKey("UserId")]
        public User? Author { get; set; }
        [ForeignKey("CommunityId")]
        public Community? Community { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
