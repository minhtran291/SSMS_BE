using SSMS.Application.Common.Models;

namespace SSMS.Application.Services.Image
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(
            UploadFile file, 
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string publicId, 
            CancellationToken cancellationToken = default);
    }
}
