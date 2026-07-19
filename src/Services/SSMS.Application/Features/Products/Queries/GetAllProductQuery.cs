using MediatR;
using SSMS.Application.DTOs.Product;
using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Products;

namespace SSMS.Application.Features.Products.Queries
{
    public record GetAllProductQuery : IRequest<List<ProductListDTO>>
    {
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductListDTO>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<List<ProductListDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            //Func<IQueryable<Product>, IQueryable<ProductListDTO>> productQuery =
            //    query =>
            //    {
            //        var ordered = query.OrderBy(p => p.CreatedOnUtc);

            //        var projected = ordered
            //            .Select(p => new
            //            {
            //                Product = p,
            //                Smallest = p.ProductSizePrices
            //                    .OrderBy(psp => psp.Size.Value)
            //                    .FirstOrDefault(),
            //                Thumbnail = p.ProductImages
            //                    .OrderBy(pi => pi.DisplayOrder)
            //                    .Select(pi => pi.Image)
            //                    .FirstOrDefault() ?? string.Empty
            //            });

            //        return projected
            //            .Select(p => new ProductListDTO
            //            {
            //                Id = p.Product.Id,
            //                ProductName = p.Product.ProductName,
            //                CategoryName = p.Product.Category.CategoryName,
            //                Thumbnail = p.Thumbnail,
            //                Price = p.Smallest != null
            //                    ? p.Smallest.Price
            //                    : 0,
            //                Size = p.Smallest != null
            //                    ? p.Smallest.Size.Value
            //                    : 0,
            //            });
            //    };

            static IQueryable<ProductListDTO> productQuery(IQueryable<Product> query)
            {
                var ordered = query.OrderBy(p => p.CreatedOnUtc);

                var projected = ordered
                    .Select(p => new
                    {
                        Product = p,
                        Smallest = p.ProductSizePrices
                            .OrderBy(psp => psp.Size.Value)
                            .FirstOrDefault(),
                        Thumbnail = p.ProductImages
                            .OrderBy(pi => pi.DisplayOrder)
                            .Select(pi => pi.Image)
                            .FirstOrDefault() ?? string.Empty
                    });

                return projected
                    .Select(p => new ProductListDTO
                    {
                        Id = p.Product.Id,
                        ProductName = p.Product.ProductName,
                        CategoryName = p.Product.Category.CategoryName,
                        Thumbnail = p.Thumbnail,
                        Price = p.Smallest != null
                            ? p.Smallest.Price
                            : 0,
                        Size = p.Smallest != null
                            ? p.Smallest.Size.Value
                            : 0,
                    });
            }

            return _productRepository
                .ListAsync(productQuery, false, cancellationToken);
        }
    }
}
