using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSMS.Application.DTOs.Product;
using SSMS.Application.Services.Product;

namespace SSMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductListDTO>>> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProducts(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDetailDTO>> GetProductById(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductById(id, cancellationToken);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("form-data")]
        public async Task<ActionResult<ProductFormDataDTO>> GetProductFormData(CancellationToken cancellationToken)
        {
            var data = await _productService.GetProductFormDataAsync(cancellationToken);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromForm] CreateProductDTO dto, CancellationToken cancellationToken = default)
        {
            var productId = await _productService.CreateProductAsync(dto, cancellationToken);
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = productId },
                new
                {
                    id = productId,
                });
        }
    }
}