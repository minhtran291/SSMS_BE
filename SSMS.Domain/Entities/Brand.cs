namespace SSMS.Domain.Entities
{
    public class Brand : BaseEntity
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
