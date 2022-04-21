﻿using Microsoft.AspNetCore.Mvc;
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

            var categories = Assert.IsAssignableFrom<Response<List<CategoryDto>>>(ok.Value);

            Assert.Equal(categories.Data.ToList().Count, _categories.Count);
        }
    }
}
