namespace SSMS.Domain.Entities
{
    public class ProductSizePrice
    {
        private decimal _price;
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public decimal Price
        {
            get => _price;
            set => _price = value < 0 
                ? throw new ArgumentOutOfRangeException(nameof(Price)) 
                : value;
        }

        public virtual Product Product { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
