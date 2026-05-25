namespace SSMS.Domain.Entities
{
    public class ProductImage 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; } = string.Empty;

        public virtual Product Product { get; set; } = null!;
    }
}
