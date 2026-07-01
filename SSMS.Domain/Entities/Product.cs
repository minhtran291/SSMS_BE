using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class Product : BaseEntity<int>, ISoftDeletedEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Brand Brand { get; set; } = null!;
        public virtual ICollection<ProductSizePrice> ProductSizePrices { get; set; } = [];
        public virtual ICollection<ProductImage> ProductImages { get; set; } = [];
    }
}
