using SSMS.Application.DTOs.Product;
using SSMS.Domain.Entities;

namespace SSMS.Application.Automapper
{
    public class ApplicationMapper : AutoMapper.Profile
    {
        public ApplicationMapper()
        {
            //CreateMap<ProductImage, ImageDTO>()
            //    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => "/images/products/" + src.Image));

            CreateMap<ProductSizePrice, SizePriceDTO>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.Value));

            CreateMap<Product, ProductDetailDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.Image)));

            CreateMap<Category, CategoryOptionDTO>();

            CreateMap<Brand, BrandOptionDTO>();

            CreateMap<Size, SizeOptionDTO>();

            CreateMap<ProductImage, ProductEditImageDTO>();

            CreateMap<ProductSizePrice, ProductEditSizePriceDTO>();

            CreateMap<Product, ProductEditDTO>();
        }
    }
}
