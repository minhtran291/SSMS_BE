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
        public Stream Stream { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }

    public class FileData
    {
        public required Stream Stream { get; init; }
        public required string FileName { get; init; }
        public string? ContentType { get; init; }
        public long Length { get; init; }
    }
}
