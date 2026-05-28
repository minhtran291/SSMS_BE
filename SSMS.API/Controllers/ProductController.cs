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
    }
}
