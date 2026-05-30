using Microsoft.AspNetCore.Http;

namespace SSMS.Application.DTOs.Product
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<CreateProductSizePriceDTO> SizePrices { get; set; } = [];
        public List<CreateProductImageDTO> Images { get; set; } = [];
    }

    public class CreateProductSizePriceDTO
    {
        public int SizeId { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductImageDTO
    {
        public IFormFile Image { get; set; } = null!;
        public int DisplayOrder { get; set; }
    }
}
