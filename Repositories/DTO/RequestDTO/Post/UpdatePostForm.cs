using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Post
{
    public class UpdatePostForm
    {
        [StringLength(300, MinimumLength = 1)]
        public string? Subject { get; set; }
        [StringLength(40000, MinimumLength = 1)]
        public string? Content { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? Status { get; set; }
    }
}
