using MediatR;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Repositories.Products;
using SSMS.Application.Common;
using SSMS.Application.Common.Enums;

namespace SSMS.Application.Features.Products.Queries
{
    public record GetAllProductQuery(ProductSearchDTO Dto) : IRequest<PagedResult<ProductListDTO>>;

    public class GetAllProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductQuery, PagedResult<ProductListDTO>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public Task<PagedResult<ProductListDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            //Func<IQueryable<Product>, IQueryable<ProductListDTO>> productQuery =
            //    query =>
            //    {
            //        if (!string.IsNullOrEmpty(request.Dto.Keyword))
            //        {
            //            co phan biet hoa thuong ko
            //            string trimmedKeyword = request.Dto.Keyword.ToLower().Trim();
            //            query = query.Where(p => p.ProductName.ToLower().Contains(trimmedKeyword));
            //        }

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

            var query = _productRepository.Query(QueryTracking.NoTracking);

            if (!string.IsNullOrEmpty(request.Dto.Keyword))
            {
                //co phan biet hoa thuong ko
                // ko nen kiem tra vi neu nguoi dung ko nhap gi thi quay ve list ban dau
                // fe phai tu chuyen doi luon ve trang 1 khi tim kiem neu ko se bi loi
                string trimmedKeyword = request.Dto.Keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(trimmedKeyword));
            }

            if (!request.Dto.IncludeDeleted)
                query = query.Where(p => !p.IsDeleted);

            var projectedQuery = query
                    .OrderByDescending(p => p.CreatedOnUtc)
                    .Select(p => new
                    {
                        Product = p,
                        Smallest = p.ProductSizePrices
                            .OrderBy(psp => psp.Size.Value)
                            .FirstOrDefault(),
                        //Thumbnail = p.ProductImages
                        //    .OrderBy(pi => pi.DisplayOrder)
                        //    .Select(pi => pi.Image)
                        //    .FirstOrDefault() ?? string.Empty
                    })
                    .Select(p => new ProductListDTO
                    {
                        Id = p.Product.Id,
                        ProductName = p.Product.ProductName,
                        CategoryName = p.Product.Category.CategoryName,
                        //Thumbnail = p.Thumbnail,
                        Price = p.Smallest != null
                            ? p.Smallest.Price
                            : 0,
                        Size = p.Smallest != null
                            ? p.Smallest.Size.Value
                            : 0,
                    });

            //static IQueryable<ProductListDTO> productQuery(IQueryable<Product> query, ProductSearchDTO search)
            //{
                //if (!string.IsNullOrEmpty(search.Keyword))
                //{
                //    //co phan biet hoa thuong ko
                //    string trimmedKeyword = search.Keyword.Trim().ToLower();
                //    query = query.Where(p => p.ProductName.ToLower().Contains(trimmedKeyword));
                //}

                //if(!search.IncludeDeleted)
                //    query = query.Where(p => !p.IsDeleted);

                //var ordered = query.OrderBy(p => p.CreatedOnUtc);

            //    var projected = query
            //        .OrderByDescending(p => p.CreatedOnUtc)
            //        .Select(p => new
            //        {
            //            Product = p,
            //            Smallest = p.ProductSizePrices
            //                .OrderBy(psp => psp.Size.Value)
            //                .FirstOrDefault(),
            //            Thumbnail = p.ProductImages
            //                .OrderBy(pi => pi.DisplayOrder)
            //                .Select(pi => pi.Image)
            //                .FirstOrDefault() ?? string.Empty
            //        });

            //    return projected
            //        .Select(p => new ProductListDTO
            //        {
            //            Id = p.Product.Id,
            //            ProductName = p.Product.ProductName,
            //            CategoryName = p.Product.Category.CategoryName,
            //            Thumbnail = p.Thumbnail,
            //            Price = p.Smallest != null
            //                ? p.Smallest.Price
            //                : 0,
            //            Size = p.Smallest != null
            //                ? p.Smallest.Size.Value
            //                : 0,
            //        });
            //}

            return _productRepository.PageAsync(
                projectedQuery,
                request.Dto,
                cancellationToken);
        }
    }
}
