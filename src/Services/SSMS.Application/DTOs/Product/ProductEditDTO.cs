using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Application.DTOs.Product
{
    public class ProductEditDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<ProductEditImageDTO> ProductImages {  get; set; } = [];
        public List<ProductEditSizePriceDTO> ProductSizePrices { get; set; } = [];
    }

    public class ProductEditImageDTO
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
    }

    public class ProductEditSizePriceDTO
    {
        public int SizeId { get; set; }
        public decimal Price { get; set; }
    }
}
