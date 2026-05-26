using SSMS.Application.DTOs.Product;
using SSMS.Domain.Entities;

namespace SSMS.Application.Automapper
{
    public class ApplicationMapper : AutoMapper.Profile
    {
        public ApplicationMapper()
        {
            CreateMap<ProductImage, ImageDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => $"/images/products/{src.Image}"));

            CreateMap<ProductSizePrice, SizePriceDTO>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
                //.ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
                //.ForMember(dest => dest.ProductSizePrices, opt => opt.MapFrom(src => src.ProductSizePrices));
        }
    }
}
