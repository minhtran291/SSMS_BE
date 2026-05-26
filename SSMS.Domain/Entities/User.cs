using Microsoft.AspNetCore.Identity;
using SSMS.Domain.Enums;

namespace SSMS.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
        public UserStatus Status { get; set; }
        public string? FullName { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public GenderType Gender {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
