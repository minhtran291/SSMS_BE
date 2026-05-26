using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Services.Base;
using SSMS.Persistence.UnitOfWork;

namespace SSMS.Application.Services.Product
{
    public class ProductService(IUnitOfWork unitOfWork,
        IMapper mapper) : Service(unitOfWork, mapper), IProductService
    {

        public async Task<IReadOnlyList<ProductDTO>> GetAllProducts(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Product
                .Query()
                .AsNoTracking()
                .AsSplitQuery()
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
