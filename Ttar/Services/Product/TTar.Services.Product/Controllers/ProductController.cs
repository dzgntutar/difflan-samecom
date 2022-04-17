using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Services;

namespace TTar.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _ProductService.GetAll();

            return new ObjectResult(categories) { StatusCode = categories.HtmlStatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Product = await _ProductService.GetById(id);  

            return new ObjectResult(Product) { StatusCode = Product.HtmlStatusCode};
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateDto ProductDto)
        {
            var newProduct = await _ProductService.Create(ProductDto);

            return new ObjectResult(newProduct) { StatusCode = newProduct.HtmlStatusCode };
        }

    }
}
