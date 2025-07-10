using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.User
{
    public class UpdateUserForm
    {
        [RegularExpression(@"^[a-zA-Z0-9\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẮẰẲẴẶắằẳẵặƯứừửữự]+$",
            ErrorMessage = "UserName must not contain special characters.")]
        public string? Username { get; set; }
        public string? TitleName { get; set; }
        [StringLength(12, ErrorMessage = "Not a typical phone number format", MinimumLength = 10)]
        [RegularExpression(@"^\+?[\d\s\-\(\)\.]+$",
            ErrorMessage = "Not a phone number format")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }

        public int? GenderId { get; set; }
    }
}
