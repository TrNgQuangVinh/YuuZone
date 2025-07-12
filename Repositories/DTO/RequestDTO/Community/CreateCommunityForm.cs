using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Community
{
    public class CreateCommunityForm
    {
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(120, MinimumLength = 1)]
        public string Description { get; set; }
        public string Guideline { get; set; }
        public DateTime CreateDate { get; set; }
        public int? StatusId { get; set; }
    }
}
