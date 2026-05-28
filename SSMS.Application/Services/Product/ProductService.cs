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
                .Select(p => new ProductListDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    BrandName = p.Brand.BrandName,
                    Thumbnail = p.ProductImages
                        .OrderBy(pi => pi.DisplayOrder)
                        .Select(pi => pi.Image)
                        .FirstOrDefault() ?? string.Empty,
                    Price = p.ProductSizePrices
                        .Min(psp => psp.Price)
                })
                .ToListAsync(cancellationToken);
        }

        /*
        Chat bao cai gi ma ko can include dung select thi
        ef core tu translate JOIN
        */
    }
}
