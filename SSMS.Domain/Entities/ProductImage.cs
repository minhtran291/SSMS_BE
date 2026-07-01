using SSMS.Domain.ExtendedEntities;

namespace SSMS.Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public int ProductId { get; set; }
        public string Image { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
