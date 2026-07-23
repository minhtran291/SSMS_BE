using SSMS.Application.Common.Models;

namespace SSMS.Application.Services.Storage
{
    public interface IFileStorageService
    {
        Task<UploadedImage> UploadAsync(
            UploadFile file, 
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string storageKey, 
            CancellationToken cancellationToken = default);
    }
}
