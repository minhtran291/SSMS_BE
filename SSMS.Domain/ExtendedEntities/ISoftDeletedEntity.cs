namespace SSMS.Domain.ExtendedEntities
{
    public interface ISoftDeletedEntity
    {
        bool IsDeleted { get; set; }
    }
}
