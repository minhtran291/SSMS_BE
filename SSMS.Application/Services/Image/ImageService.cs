using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SSMS.Application.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveProductImageAsync(
            IFormFile file,
            CancellationToken cancellationToken = default)
        {
            if (file.Length == 0)
                throw new ArgumentException("Ảnh không hợp lệ!");

            var allowedExtensions = new[]
            {
                ".jpg", 
                ".jpeg", 
                ".png", 
                ".webp"
            };

            var extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Định dạng ảnh không hợp lệ!");

            var fileName = $"{Guid.NewGuid():N}{extension}";
            // :N de loai bo dau gach ngang trong guid

            var folderPath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "products"
            );

            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            return Path.Combine(
                "images",
                "products",
                fileName)
                .Replace("\\", "/");
        }
    }
}
