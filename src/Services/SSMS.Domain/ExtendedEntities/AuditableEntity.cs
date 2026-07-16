namespace SSMS.Domain.ExtendedEntities
{
    public class AuditableEntity
    {
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
