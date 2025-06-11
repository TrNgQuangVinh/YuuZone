using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO
{
    public class UpdateUserForm
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẮẰẲẴẶắằẳẵặƯứừửữự]+$",
            ErrorMessage = "UserName must not contain special characters.")]
        public string Username { get; set; }
        public string? TitleName { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "Not a typical phone number format", MinimumLength = 10)]
        [RegularExpression(@"^\+?[\d\s\-\(\)\.]+$",
            ErrorMessage = "Not a phone number format")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        [Required]
        public int GenderId { get; set; }
    }
}
