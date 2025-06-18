using Repositories.DTO.ResponseDTO.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Post
{
    public class CreatePostForm
    {
        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Subject { get; set; }
        [Required]
        [StringLength(40000, MinimumLength = 1)]
        public string Content { get; set; }

        public string? AuthorName { get; set; }
        public Guid? AuthorId { get; set; }
        public string? CommunityName { get; set; }
        public Guid? CommunityId { get; set; }
    }
}
