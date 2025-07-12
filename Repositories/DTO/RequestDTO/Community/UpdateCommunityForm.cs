using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Community
{
    public class UpdateCommunityForm
    {
        [StringLength(40, MinimumLength = 1)]
        public string? Name { get; set; }
        [StringLength(120, MinimumLength = 1)]
        public string? Description { get; set; }
        public string? Guideline { get; set; }
        public string? ImageBanner { get; set; }
        public string? ImageIcon { get; set; }
    }
}
