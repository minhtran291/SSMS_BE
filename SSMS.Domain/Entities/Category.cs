namespace SSMS.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
