using FluentValidation;
using SSMS.Application.DTOs.Product;

namespace SSMS.Application.Validator.Product
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Tên sản phẩm không được để trống")
                .MaximumLength(200)
                .WithMessage("Tên sản phẩm không được vượt quá 200 ký tự");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("Vui lòng chọn một danh mục hợp lệ");

            RuleFor(x => x.BrandId)
                .GreaterThan(0)
                .WithMessage("Vui lòng chọn một thương hiệu hợp lệ");

            RuleFor(x => x.SizePrices)
                .NotEmpty()
                .WithMessage("Vui lòng thêm ít nhất một kích thước và giá")
                .Must(x => x.Select(s => s.SizeId).Distinct().Count() == x.Count)
                .WithMessage("Kích thước không được trùng lặp");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("Vui lòng thêm ít nhất một hình ảnh sản phẩm")
                .Must(x => x.Select(i => i.DisplayOrder).Distinct().Count() == x.Count)
                .WithMessage("Thứ tự hiển thị ảnh không được trùng lặp");

            RuleForEach(x => x.SizePrices)
                .SetValidator(new CreateProductSizePriceDTOValidator());

            RuleForEach(x => x.Images)
                .SetValidator(new CreateProductImageDTOValidator());
        }
    }

    public class CreateProductSizePriceDTOValidator : AbstractValidator<CreateProductSizePriceDTO>
    {
        public CreateProductSizePriceDTOValidator()
        {
            RuleFor(x => x.SizeId)
                .GreaterThan(0)
                .WithMessage("Vui lòng chọn một kích thước hợp lệ");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1000)
                .WithMessage("Giá tối thiểu là 1000 đồng");
        }
    }

    public class CreateProductImageDTOValidator : AbstractValidator<CreateProductImageDTO>
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg", 
            ".jpeg", 
            ".png", 
            ".webp"
        };

        public CreateProductImageDTOValidator()
        {
            RuleFor(x => x.Image)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Hình ảnh không được để trống")

                .Must(file => file.Length > 0)
                .WithMessage("Hình ảnh không được để trống")
                .Must(file =>
                {
                    return AllowedExtensions.Contains(
                        Path.GetExtension(file.FileName), 
                        StringComparer.OrdinalIgnoreCase);
                })
                .WithMessage("Định dạng ảnh không hợp lệ")

                .Must(file => file.Length <= 5 * 1024 * 1024)
                .WithMessage("Kích thước ảnh tối đa là 5MB");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Thứ tự hiển thị phải lớn hơn hoặc bằng 0");
        }
    }
}
