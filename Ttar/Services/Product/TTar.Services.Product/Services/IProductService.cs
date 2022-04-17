using Ttar.Common.Models;
using TTar.Services.Product.Models.Dto;

namespace TTar.Services.Product.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductGetDto>>> GetAll();
        Task<Response<ProductGetDto>> GetById(string id);
        Task<Response<ProductCreateDto>> Create(ProductCreateDto product);
    }
}
