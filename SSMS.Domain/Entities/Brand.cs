using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class Brand : BaseEntity<int>, ISoftDeletedEntity
    {
        public string BrandName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
