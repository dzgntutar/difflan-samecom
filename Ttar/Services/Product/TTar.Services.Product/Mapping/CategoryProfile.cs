using AutoMapper;
using TTar.Services.Product.Models.Dto;
using TTar.Services.Product.Models.Entities;

namespace TTar.Services.Product.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
