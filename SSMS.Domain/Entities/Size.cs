using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class Size : BaseEntity<int>, ISoftDeletedEntity
    {
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<ProductSizePrice> ProductSizePrices { get; set; } = [];
    }
}
