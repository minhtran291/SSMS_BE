using FluentValidation;
using SSMS.Application.DTOs.Product;

namespace SSMS.Application.Validator.Product
{
    public class UpdateProductDTOValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator()
        {
            //Include(new CreateProductDTOValidator());

            //RuleFor(x => x.Id)
            //    .GreaterThan(0)
            //    .WithMessage("Id sản phẩm không hợp lệ");
        }
    }
}
