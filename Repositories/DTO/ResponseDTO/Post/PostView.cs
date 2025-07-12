using Repositories.DTO.ResponseDTO.Comment;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.ResponseDTO.Post
{
    public class PostView
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        
        public DateTime PostedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string Author { get; set; }
        public string Community{ get; set; }

        public string Status { get; set; }

        public ICollection<CommentView>? Comments { get; set; }
    }
}
