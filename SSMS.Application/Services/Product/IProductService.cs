using SSMS.Application.DTOs.Product;

namespace SSMS.Application.Services.Product
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductListDTO>> GetAllProducts(CancellationToken cancellationToken = default);
        Task<ProductDetailDTO?> GetProductById(int id, CancellationToken cancellationToken = default);
        Task<ProductFormDataDTO> GetProductFormDataAsync(CancellationToken cancellationToken = default);
    }
}
