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
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }

        [ForeignKey("UserId")]
        public User Author { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
