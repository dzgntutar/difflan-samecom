using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Services;

namespace TTar.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAll();
            return new ObjectResult(categories) { StatusCode = categories.HtmlStatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _categoryService.GetById(id);  

            return new ObjectResult(category) { StatusCode = category.HtmlStatusCode};
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryDto categoryDto)
        {
            var newCategory = await _categoryService.Create(categoryDto);

            return new ObjectResult(newCategory) { StatusCode = newCategory.HtmlStatusCode };
        }

    }
}
