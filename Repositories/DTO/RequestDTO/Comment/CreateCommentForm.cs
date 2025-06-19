using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Comment
{
    public class CreateCommentForm
    {
        [StringLength(10000, MinimumLength = 1)]
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
