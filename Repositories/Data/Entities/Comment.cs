using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }

        [ForeignKey("UserId")]
        public User Author { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
