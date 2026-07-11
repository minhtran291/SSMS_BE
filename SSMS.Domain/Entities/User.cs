using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string KeycloakId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }
}
