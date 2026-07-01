using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class Category : BaseEntity<int>, ISoftDeletedEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
