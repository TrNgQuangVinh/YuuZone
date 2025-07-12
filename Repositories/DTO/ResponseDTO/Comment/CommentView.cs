using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.ResponseDTO.Comment
{
    public class CommentView
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        
        public string Author { get; set; }
    }
}
