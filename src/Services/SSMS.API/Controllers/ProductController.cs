using Microsoft.AspNetCore.Mvc;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Features.Products.Queries;

namespace SSMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        [HttpPost("search")]
        public async Task<IActionResult> GetAllProducts([FromBody] ProductSearchDTO dto, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllProductQuery(dto), cancellationToken);
            return Ok(result);
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<ProductDetailDTO>> GetProductById(int id, CancellationToken cancellationToken)
        //{
        //    var product = await _productService.GetProductById(id, cancellationToken);

        //    return Ok(product);
        //}

        //[HttpGet("form-data")]
        //public async Task<ActionResult<ProductFormDataDTO>> GetProductFormData(CancellationToken cancellationToken)
        //{
        //    var data = await _productService.GetProductFormDataAsync(cancellationToken);
        //    return Ok(data);
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateProduct([FromForm] CreateProductDTO dto, CancellationToken cancellationToken = default)
        //{
        //    var productId = await _productService.CreateProductAsync(dto, cancellationToken);
        //    return CreatedAtAction(
        //        nameof(GetProductById),
        //        new { id = productId },
        //        new
        //        {
        //            id = productId,
        //        });
        //}

        //[HttpGet("{id:int}/edit")]
        //public async Task<ActionResult<ProductEditDTO>> GetProductDataEditForm(int id, CancellationToken cancellationToken = default)
        //{
        //    var productEditForm = await _productService.GetProductDataEditFormAsync(id, cancellationToken);

        //    return Ok(productEditForm);
        //}
    }
}