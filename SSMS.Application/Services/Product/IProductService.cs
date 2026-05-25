using SSMS.Application.DTOs.Product;

namespace SSMS.Application.Services.Product
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDTO>> GetAllProducts(CancellationToken cancellationToken = default);
    }
}
