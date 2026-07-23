using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using SSMS.Application.Common.Models;
using SSMS.Application.Exceptions;
using SSMS.Application.Services.Storage;
using SSMS.Infrastructure.Configurations;

namespace SSMS.Infrastructure.Services.Storage
{
    public class CloudinaryFileStorageService : IFileStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;
        public CloudinaryFileStorageService(IOptions<CloudinarySettings> options)
        {
            _cloudinarySettings = options.Value;

            if (string.IsNullOrWhiteSpace(_cloudinarySettings.CloudName) ||
                string.IsNullOrWhiteSpace(_cloudinarySettings.ApiKey) ||
                string.IsNullOrWhiteSpace(_cloudinarySettings.ApiSecret))
                throw new InvalidOperationException("Cloudinary configuration is invalid.");

            var account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
                );

            _cloudinary = new Cloudinary(account);
        }
        private static readonly string[] AllowedExtensions =
        [
            ".jpg",
            ".jpeg",
            ".png",
            ".webp",
        ];

        public async Task<UploadedImage> UploadAsync(UploadFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                throw new BadRequestException("Ảnh không hợp lệ.");

            var extension = Path.GetExtension(file.FileName);

            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                throw new BadRequestException("Định dạng ảnh không hợp lệ.");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.Stream),
                Folder = $"{_cloudinarySettings.RootFolder}/products",
                UseFilename = false,
                Overwrite = false
            };

            var result = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (result.Error is not null)
                throw new ExternalServiceException(
                    "Failed to upload image.",
                    new Exception(result.Error.Message));

            return new UploadedImage
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                StorageKey = result.PublicId
            };
        }

        public async Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(storageKey))
                return;

            var deleteParams = new DeletionParams(storageKey);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Error is not null)
                throw new ExternalServiceException(
                    "Failed to upload image.",
                    new Exception(result.Error.Message));
        }
    }
}