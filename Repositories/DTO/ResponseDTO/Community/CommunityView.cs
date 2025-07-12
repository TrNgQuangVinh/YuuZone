using Repositories.DTO.ResponseDTO.Comment;
using Repositories.DTO.ResponseDTO.Post;
using Repositories.DTO.ResponseDTO.Tag;
using Repositories.DTO.ResponseDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.ResponseDTO.Community
{
    public class CommunityView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Guideline { get; set; }
        public string? ImageBanner { get; set; }
        public string? ImageIcon { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<PostView>? Posts { get; set; }
        public ICollection<TagView>? Tags { get; set; }
        public ICollection<UserView>? Members { get; set; }
    }
}
