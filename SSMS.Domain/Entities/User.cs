using SSMS.Domain.Enums;
using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string KeycloakId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public GenderType Gender {  get; set; }
        public bool IsSuperAdmin { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
