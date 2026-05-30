using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Services.Base;
using SSMS.Application.Services.Image;
using SSMS.Domain.Entities;
using SSMS.Persistence.UnitOfWork;

namespace SSMS.Application.Services.Product
{
    public class ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper, 
        IImageService imageService) : Service(unitOfWork, mapper), IProductService
    {
        private readonly IImageService _imageService = imageService;
        public async Task<IReadOnlyList<ProductListDTO>> GetAllProducts(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Product
                .Query()
                .AsNoTracking()
                .Select(p => new
                {
                    Product = p,
                    Cheapest = p.ProductSizePrices
                        .OrderBy(psp => psp.Price)
                        .FirstOrDefault()
                })
                .Select(p => new ProductListDTO
                {
                    Id = p.Product.Id,
                    ProductName = p.Product.ProductName,
                    CategoryName = p.Product.Category.CategoryName,
                    BrandName = p.Product.Brand.BrandName,
                    Thumbnail = p.Product.ProductImages
                        .OrderBy(pi => pi.DisplayOrder)
                        .Select(pi => "/images/products/" + pi.Image)
                        .FirstOrDefault() ?? string.Empty,
                    Price = p.Cheapest != null
                        ? p.Cheapest.Price
                        : 0,
                    Size = p.Cheapest != null
                        ? p.Cheapest.Size.Value
                        : 0,
                })
                .ToListAsync(cancellationToken);
        }

        /*
        Chat bao cai gi ma ko can include dung select thi
        ef core tu translate JOIN
        */

        public async Task<ProductDetailDTO?> GetProductById(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Product
                .Query()
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ProductFormDataDTO> GetProductFormDataAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _unitOfWork.Category
                .Query()
                .AsNoTracking()
                .ProjectTo<CategoryOptionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var brands = await _unitOfWork.Brand
                .Query()
                .AsNoTracking()
                .ProjectTo<BrandOptionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var sizes = await _unitOfWork.Size
                .Query()
                .AsNoTracking()
                .ProjectTo<SizeOptionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProductFormDataDTO
            {
                Categories = categories,
                Brands = brands,
                Sizes = sizes
            };
        }

        public async Task CreateProductAsync(CreateProductDTO dto, CancellationToken cancellationToken = default)
        {
            var checkProductName = await _unitOfWork.Product
                .Query()
                .AnyAsync(p => string.Equals(p.ProductName, dto.ProductName, StringComparison.OrdinalIgnoreCase), cancellationToken);
                //p.ProductName.Equals(dto.ProductName, StringComparison.OrdinalIgnoreCase), cancellationToken);

            if (checkProductName)
                throw new Exception("Tên sản phẩm đã tồn tại!");

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var product = new Domain.Entities.Product
                {
                    ProductName = dto.ProductName,
                    Description = dto.Description,
                    CategoryId = dto.CategoryId,
                    BrandId = dto.BrandId,
                };

                await _unitOfWork.Product.AddAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var productSizePrices = dto.SizePrices.Select(x => new ProductSizePrice
                {
                    ProductId = product.Id,
                    SizeId = x.SizeId,
                    Price = x.Price
                }).ToList();

                await _unitOfWork.ProductSizePrice.AddRangeAsync(productSizePrices, cancellationToken);

                var productImages = new List<ProductImage>();
                
                foreach (var imageDto in dto.Images)
                {
                    var fileName = await _imageService
                        .SaveProductImageAsync(imageDto.Image, cancellationToken);
                    
                    productImages.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        Image = fileName,
                        DisplayOrder = imageDto.DisplayOrder
                    });
                }

                await _unitOfWork.ProductImage.AddRangeAsync(productImages, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
