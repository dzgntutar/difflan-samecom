using Ttar.Common.Models;
using TTar.Services.Product.Models.Dto;

namespace TTar.Services.Product.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAll();
        Task<Response<CategoryDto>> GetById(string id);
        Task<Response<CategoryDto>> Create(CategoryDto category);
    }
}
