using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Data.Entities
{
    public class Community
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Guideline { get; set; }
        public string ImageBanner { get; set; }
        public string ImageIcon { get; set; }
        public DateTime CreateDate { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
