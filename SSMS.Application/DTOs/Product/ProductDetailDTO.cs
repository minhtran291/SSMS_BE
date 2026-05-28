using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Application.DTOs.Product
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        //public List<ImageDTO> ProductImages { get; set; } = [];
        public List<string> ProductImages { get; set; } = [];
        public List<SizePriceDTO> ProductSizePrices { get; set; } = [];
    }

    public class ImageDTO
    {
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class SizePriceDTO
    {
        public int Size { get; set; }
        public decimal Price { get; set; }
    }
}
