using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DTO.RequestDTO.Community
{
    public class JoinCommunityForm
    {
        public string? Username { get; set; }
        public Guid? UserId { get; set; }
        public string? CommunityName { get; set; }
        public Guid? CommunityId { get; set; }

        /*public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            if (string.IsNullOrWhiteSpace(CommunityName) && !CommunityId.HasValue)
                yield return new ValidationResult(
                    "Send either CommunityName or CommunityId.",
                    new[] { nameof(CommunityName), nameof(CommunityId) });

            if (string.IsNullOrWhiteSpace(Username) && !UserId.HasValue)
                yield return new ValidationResult(
                    "Send either Username or UserId.",
                    new[] { nameof(Username), nameof(UserId) });
        }*/
    }
}
