using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SSMS.Application.Exceptions;

namespace SSMS.Application.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /* IWebHostEnvironment cung cap thong tin moi truong chay cua ASP.NET Core
        _webHostEnvironment.WebRootPath co the tra ve D:\Products\SSMS\SSMS.API\wwwroot
        */

        private static readonly string[] AllowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        public async Task<string> SaveProductImageAsync(
            IFormFile file,
            CancellationToken cancellationToken = default)
        {
            if (file is null || file.Length == 0)
                throw new BadRequestException("Ảnh không hợp lệ!");

            var extension = Path.GetExtension(file.FileName);

            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                throw new BadRequestException("Định dạng ảnh không hợp lệ!");

            var fileName = $"{Guid.NewGuid():N}{extension}";
            // :N de loai bo dau gach ngang trong guid

            var folderPath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "products"
            );

            // tao duong dan thu muc
            // D:\Projects\SSMS\SSMS.API\wwwroot\images\products

            Directory.CreateDirectory(folderPath);

            // tao thu muc neu chua ton tai, chua co thi tao, co roi thi khong lam gi

            var filePath = Path.Combine(folderPath, fileName);

            // tao duong dan file hoan chinh: duong dan thu muc + ten file

            await using var stream = new FileStream(filePath, FileMode.Create);

            // mo FileStream de ghi du lieu
            // FileMode.Create: neu chua co -> tao moi, neu da co -> ghi de len

            await file.CopyToAsync(stream, cancellationToken);

            // copy du lieu anh vao file

            return Path.Combine(
                "images",
                "products",
                fileName)
                .Replace("\\", "/");

            // tra ve duong dan luu db
            // doi \\ -> / de hop voi url
        }
    }
}
