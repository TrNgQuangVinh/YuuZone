using Repositories.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string? TitleName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? ImageAvatar { get; set; }
        public string? ImageBanner { get; set; }

        [Required]
        public int RoleId { get; set; }
        [Required]
        public int GenderId { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        [ForeignKey("GenderId")]
        public Gender? Gender { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
