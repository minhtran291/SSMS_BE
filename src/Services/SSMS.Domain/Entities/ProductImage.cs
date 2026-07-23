using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class ProductImage : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string StorageKey { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
