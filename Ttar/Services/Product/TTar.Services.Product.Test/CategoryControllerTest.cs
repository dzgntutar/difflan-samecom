using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ttar.Common.Models;
using TTar.Services.Product.Controllers;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Services;
using Xunit;

namespace TTar.Services.Product.Test
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoryController _categoryController;

        private readonly List<CategoryDto> _categories;

        public CategoryControllerTest()
        {
            _mockService = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_mockService.Object);
            _categories = new List<CategoryDto>()
            {
                new  CategoryDto{Id = "1",Name ="Category1"},
                new  CategoryDto{Id = "2",Name ="Category2"},
                new  CategoryDto{Id = "3",Name ="Category3"},
                new  CategoryDto{Id = "4",Name ="Category4"},
            };
        }


        [Fact]
        public async void GetAllCategory_ExecuteAction_ReturnDataWithOkMessage()
        {
            _mockService.Setup(x => x.GetAll()).ReturnsAsync(Response<List<CategoryDto>>.Success(_categories, 200));

            var result = await _categoryController.Get();

            var ok = Assert.IsType<ObjectResult>(result);

            var responseData = Assert.IsAssignableFrom<Response<List<CategoryDto>>>(ok.Value);

            Assert.Equal(responseData.Data.ToList().Count, _categories.Count);
        }

        [Theory]
        [InlineData("0")]
        public async void GetCategoryById_InvalidId_ReturnNotFound(string id)
        {
            var data = Response<CategoryDto>.Fail("Category not found", 404);

            _mockService.Setup(x => x.GetById(id)).ReturnsAsync(data);

            var result = await _categoryController.Get(id);

            var notFound = Assert.IsType<ObjectResult>(result);

            var responseData = Assert.IsAssignableFrom<Response<CategoryDto>>(notFound.Value);

            Assert.NotNull(responseData.Errors);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public async void GetCategoryById_ValidId_ReturnNotFound(string id)
        {
            var category = _categories.FirstOrDefault(_ => _.Id == id) ?? new CategoryDto(); 

            var data = Response<CategoryDto>.Success(category, 200);

            _mockService.Setup(x => x.GetById(id)).ReturnsAsync(data);

            var result = await _categoryController.Get(id);

            var message = Assert.IsType<ObjectResult>(result);

            var responseData = Assert.IsAssignableFrom<Response<CategoryDto>>(message.Value);

            Assert.Equal(responseData.Data.Id,category.Id);
            Assert.Equal(responseData.Data.Name, category.Name);

        }

        [Fact]
        public async void InsertCategory_Execute_ReturnCreatedAtAction()
        {
            var category = _categories.First();

            var data = Response<CategoryDto>.Success(category, 201);

            _mockService.Setup(x => x.Create(category)).ReturnsAsync(data);

            var result = await _categoryController.Post(category);

            var createdAtAction = Assert.IsType<ObjectResult>(result);

            _mockService.Verify(x => x.Create(category), Times.Once);

            Assert.Equal(201, createdAtAction.StatusCode);
        }
    }
}
