using SSMS.Application.DTOs.Product;

namespace SSMS.Application.Services.Image
{
    public interface IImageService
    {
        Task<string> SaveProductImageAsync(
            FileData file, 
            CancellationToken cancellationToken = default);
    }
}
