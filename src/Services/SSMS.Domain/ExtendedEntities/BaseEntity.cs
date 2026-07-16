namespace SSMS.Domain.ExtendedEntities
{
    public abstract class BaseEntity<TKey> : AuditableEntity
    {
        public TKey Id { get; set; } = default!;
    }
}
