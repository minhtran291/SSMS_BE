using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Services.Base;
using SSMS.Persistence.UnitOfWork;

namespace SSMS.Application.Services.Product
{
    public class ProductService(IUnitOfWork unitOfWork,
        IMapper mapper) : Service(unitOfWork, mapper), IProductService
    {

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
    }
}
