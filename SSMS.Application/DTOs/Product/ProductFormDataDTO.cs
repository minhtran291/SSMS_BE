namespace SSMS.Application.DTOs.Product
{
    public class ProductFormDataDTO
    {
        public List<CategoryOptionDTO> Categories { get; set; } = [];
        public List<BrandOptionDTO> Brands { get; set; } = [];
        public List<SizeOptionDTO> Sizes { get; set; } = [];
    }

    public class CategoryOptionDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class BrandOptionDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }

    public class SizeOptionDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
    }
}
